using Bitso.Entities.WebSocketApi;
using Newtonsoft.Json;
using System;
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
        private ClientWebSocket _ws;


        //Object _obj;
        public string Result { get; set; }
        
        private SocketClass()
        {
            _ws = new ClientWebSocket();
            tokenSource = new CancellationTokenSource();
            _uri = new Uri("wss://ws.bitso.com");
            _caltoken = tokenSource.Token;
            _subscription = new Subscription();
        }

        /// <summary>
        /// Returns the instance of the object
        /// </summary>
        /// <returns></returns>

        public static SocketClass GetInstance()
        {
            return _instance;
        }
        
        /// <summary>
        /// Close the current thread of the receive socket.
        /// </summary>
        public void Close()
        {
            tokenSource.Cancel();
        }

        /// <summary>
        /// Establish the connection to the socket
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync(string book, string type)
        {
            _subscription.book = book;
            _subscription.type = type;

            try
            {
                await _ws.ConnectAsync(_uri, _caltoken);
                await SubscribeAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Subscribe the client to receive specific data.
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="sub"></param>
        /// <returns></returns>

        private async Task SubscribeAsync()
        {
            try
            {
                while (_ws.State == WebSocketState.Connecting) ;
                string json = JsonConvert.SerializeObject(_subscription);
                byte[] buffer = Encoding.ASCII.GetBytes(json);
                await _ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Binary, true, _caltoken);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Function to receive the information from a subscription.
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>

        public async Task<string> Receive()
        {
            try
            {
                _buffer3 = new ArraySegment<byte>(new Byte[1024]);
                while (_ws.State == WebSocketState.Connecting);
                _result = await _ws.ReceiveAsync(_buffer3, _caltoken);
                //if (_result.EndOfMessage) return "";
                return encoder.GetString(_buffer3.Array);
            }

            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return "";
        }

        /// <summary>
        /// Check if the connection to the socket was established
        /// </summary>
        /// <returns></returns>

        public bool IsConnectedToSocket()
        {
            return _ws.State == WebSocketState.Open;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DisconnectAsync()
        {
            await IsReceivingMessagesAsync();
            await _ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Quit", CancellationToken.None);
            await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Quit", CancellationToken.None);
            if (_ws.State == WebSocketState.Closed) return true;
            else return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsReceivingMessagesAsync()
        {
            _buffer3 = new ArraySegment<byte>(new Byte[1024]);
            _result = await _ws.ReceiveAsync(_buffer3, _caltoken);
            if (_result != null) return true;
            else return false;
        }
    }
}