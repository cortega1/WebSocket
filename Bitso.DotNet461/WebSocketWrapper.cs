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
        /*private IPAddress _destination;
        private Socket _socket;*/
        //private string _message = "{ action: 'subscribe', book: 'btc_mxn', type: 'trades' }";
        private static readonly SocketClass _instance = new SocketClass();
        //private ClientWebSocket _ws;
        private Uri _uri;
        private Subscription _subscription = new Subscription();
        private WebSocketReceiveResult _result;
        private ArraySegment<Byte> _buffer3;
        public CancellationToken _caltoken;
        public CancellationTokenSource tokenSource;

        Object _obj;
        public Object Result {
            get { return _obj; }
        }
        
        private SocketClass()
        {
            /*_destination = Dns.GetHostAddresses("wss://ws.bitso.com")[0];
            byte[] buffer = Encoding.ASCII.GetBytes(_message);
            _socket = new Socket(SocketType.Dgram, ProtocolType.Tcp);
            _socket.SendTo(buffer, _destination);*/

            /*_ws = new ClientWebSocket();
            _uri = new Uri("wss://ws.bitso.com");*/
            tokenSource = new CancellationTokenSource();
            _caltoken = tokenSource.Token;
        }


        public static SocketClass GetInstance()
        {
            return _instance;
        }

        public void Close() {
            tokenSource.Cancel();
        }

        public async Task<ArraySegment<byte>> Connect()
        {
            /*string apiUrl = "wss://ws.bitso.com";
            var data = new {  action = "subscribe", book = "btc_mxn", type = "trades" };
            string requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            string responseJson = string.Empty;

            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                byte[] response = client.UploadData(apiUrl, Encoding.UTF8.GetBytes(requestJson));

                responseJson = Encoding.UTF8.GetString(response);
                int i = 0;
            }*/

            _subscription.action = "subscribe";
            _subscription.book = "btc_mxn";
            _subscription.type = "trades";
            var ws = new System.Net.WebSockets.ClientWebSocket();
            //Task.WhenAll(new List<Task> { ConnectAsync(ws), Receive(ws) });
            await ConnectAsync(ws);
            await Receive(ws);

            return _buffer3;
            //Console.WriteLine(responseJson);

            /*await _ws.ConnectAsync(_uri, CancellationToken.None);
            byte[] buffer = Encoding.ASCII.GetBytes(_message);
            var segment = new ArraySegment<byte>(buffer);
            var result = await _ws.ReceiveAsync(segment, CancellationToken.None);
            await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Hola", CancellationToken.None);*/
            //System.Diagnostics.Debugger.Break();
            /*int count = result.Count;
            segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
            result = await _ws.ReceiveAsync(segment, CancellationToken.None);*/
        }

        private async Task ConnectAsync(ClientWebSocket socket)
        {
            string json = JsonConvert.SerializeObject(_subscription);
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            var buffer2 = new ArraySegment<byte>(buffer);
            await socket.ConnectAsync(new Uri("wss://ws.bitso.com"), CancellationToken.None);
            while (socket.State == WebSocketState.Connecting);
            if(!socket.SendAsync(buffer2, WebSocketMessageType.Binary, false, CancellationToken.None).Wait(5000))
            {
                throw new Exception("Error: Websocket send timeout");
            }
        }

        public async Task<object> ReadDataAsync() {
            var t = new Task<object>(() =>
            {
                while (_result == null) ;
                this.Close();
                return _result;
            });
            return await t;
        }

        private async Task Receive(ClientWebSocket socket)
        {
            try
            {
                //socket.ReceiveAsync()
                _buffer3 = new ArraySegment<byte>(new Byte[8192]);  //WebSocket.CreateClientBuffer(1024,1024);
                while (socket.State == WebSocketState.Connecting);

                if (socket.State == WebSocketState.Open)
                {
                    while(!_caltoken.IsCancellationRequested || socket.State == WebSocketState.Closed)
                    {
                        _result = await socket.ReceiveAsync(_buffer3, _caltoken);
                    }
                    //_obj = _result != null ? _result : null;
                    //Console.WriteLine(result.Count);
                }
            }

            catch (Exception e)
            {
                throw;
            }
        }
    }
}