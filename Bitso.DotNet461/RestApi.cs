using Bitso.Entities.Api.AvailableBooksApi;
using Bitso.Entities.Api.OrderBookApi;
using Bitso.Entities.Api.TickerApi;
using Bitso.Entities.Api.TradesApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bitso
{

    /// <summary>
    /// Class that access Bitso Public Apis.
    /// </summary>
    public class RestApi
    {
        private static HttpClient _client = new HttpClient();
        private string _baseUrl = "https://api-dev.bitso.com/v3/";
        private static readonly RestApi _instance = new RestApi();

        /// <summary>
        /// Creates an instance of Information class.
        /// </summary>
        private RestApi()
        {
            _client.BaseAddress = new Uri(_baseUrl);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static RestApi GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// Request the list book.
        /// </summary>
        /// <returns>The list book.</returns>
        public async Task<ICollection<Book>> ListBooksAsync()
        {
            BooksResponse content = null;
            HttpResponseMessage response = await JSONcallerAsync("available_books");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                content = JsonConvert.DeserializeObject<BooksResponse>(result);
            }
            return content.payload;
        }
        /// <summary>
        /// Show the ticket of a book.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<Ticker> TickerAsync(string book)
        {
            TickerResponse respuesta = null;
            HttpResponseMessage response = await JSONcallerAsync("ticker/?book=" + book);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                respuesta = JsonConvert.DeserializeObject<TickerResponse>(result);
            }
            return respuesta.payload;
        }

        /// <summary>
        /// Check for the orders of a book.
        /// </summary>
        /// <param name="book">Is the name of the book</param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public async Task<OrderBookPayload> OpenOrderBookAsync(string book, bool aggregate = true)
        {
            OrderBook orderbook = null;
            HttpResponseMessage response = null;
            if (aggregate) response = await JSONcallerAsync("order_book/?book=" + book);
            else response = await JSONcallerAsync("order_book/?book=" + book + "&aggregate=false");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                orderbook = JsonConvert.DeserializeObject<OrderBook>(result);
            }

            return orderbook.payload;
        }
        /// <summary>
        /// Check for the book trades.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="marker"></param>
        /// <param name="sort"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<ICollection<Trades>> BookTradesAsync(string book, int marker = 0, int limit = 0, string sort = null)
        {
            string uri = "trades/?book="+book;

            if (marker > 0)
            {
                uri += "&marker=" + marker;
            }

            if (!string.IsNullOrEmpty(sort))
            {
                uri += "&sort=asc";
            }

            if (limit > 0 && limit <= 100)
            {
                uri += "&limit=" + limit;
            }

            TradesResponse traderesponse = null;
            HttpResponseMessage response = await JSONcallerAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                traderesponse = JsonConvert.DeserializeObject<TradesResponse>(result);
            }

            return traderesponse.payload;
        }

        private async Task<HttpResponseMessage> JSONcallerAsync(string url)
        {
            return await _client.GetAsync(url);
        }
    }
}
