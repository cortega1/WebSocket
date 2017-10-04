using Bitso.DotNet461.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bitso
{
    public class SocketClass
    {
        private static readonly SocketClass _instance = new SocketClass();
        private Uri _uri;
        private Subscription _subscription;
        private WebSocketReceiveResult _result;
        private ArraySegment<Byte> _buffer3;
        private UTF8Encoding encoder = new UTF8Encoding();
        public CancellationToken _caltoken;
        public CancellationTokenSource tokenSource;

        //Object _obj;
        public string Result { get; set; }
        
        private SocketClass()
        {
            tokenSource = new CancellationTokenSource();
            _uri = new Uri("wss://ws.bitso.com");
            _caltoken = tokenSource.Token;
            _subscription = new Subscription();
        }


        public static SocketClass GetInstance()
        {
            return _instance;
        }

        public void Close()
        {
            tokenSource.Cancel();
        }

        public async Task ConnectAsync()
        {
            _subscription.action = "subscribe";
            _subscription.book = "btc_mxn";
            _subscription.type = "trades";

            try
            {
                var ws = new System.Net.WebSockets.ClientWebSocket();

                await ws.ConnectAsync(_uri, _caltoken);
                await Task.WhenAll(Receive(ws), SubscribeAsync(ws, _subscription));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        private async Task SubscribeAsync(ClientWebSocket socket, Subscription sub)
        {
            try
            {
                while (socket.State == WebSocketState.Connecting) ;
                string json = JsonConvert.SerializeObject(_subscription);
                byte[] buffer = Encoding.ASCII.GetBytes(json);
                await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, _caltoken);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        private async Task Receive(ClientWebSocket socket)
        {
            try
            {
                _buffer3 = new ArraySegment<byte>(new Byte[1024]);
                while (socket.State == WebSocketState.Connecting);

                while (socket.State == WebSocketState.Open)
                {
                    _result = await socket.ReceiveAsync(_buffer3, _caltoken);
                    Result = encoder.GetString(_buffer3.Array);
                    if (_result.EndOfMessage) break;
                }
            }

            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        /*public void ReadData()
        {
            var t = new Task(() =>
            {
                while (_result == null) ;
                Close();
            });
            t.Start();
        }*/
    }
}