using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Bitso.Entities;

namespace Bitso
{

    public class Information
    {
        static HttpClient _client = new HttpClient();
        string base_url = "https://api.bitso.com/v3/";

        public async Task<ICollection<Book>> ListBooks()
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

        public async Task<Ticker> Ticker(string book)
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

        private async Task<HttpResponseMessage> JSONcallerAsync(string url)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return await _client.GetAsync(base_url + url);
        }
    }
}
