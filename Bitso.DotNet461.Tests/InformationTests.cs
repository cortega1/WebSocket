using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bitso.DotNet461.Tests
{
    [TestFixture]
    public class InformationTests
    {
        [Test]
        public async Task Getbooks_NoParams_Success()
        {
            var client = Information.GetInstance();
            var books = await client.ListBooksAsync();
            Assert.IsNotNull(books);
            Assert.IsTrue(books.Count() > 0);
        }

        [Test]
        public async Task Ticker_NoParams_Success()
        {
            var client = Bitso.Information.GetInstance();
            var ticker = await client.TickerAsync("btc_mxn");
            Assert.IsNotNull(ticker);
            Assert.IsTrue(ticker.book == "btc_mxn");
        }

        [Test]
        public async Task OpenOrderBook_NoParams_Success()
        {
            var client = Bitso.Information.GetInstance();
            var openorder = await client.OpenOrderBookAsync("btc_mxn", false);
            Assert.IsNotNull(openorder);
            Assert.IsTrue(openorder.asks.Count() > 0);
        }

        [Test]
        public async Task BookTrades_NoParams_Success()
        {
            var client = Bitso.Information.GetInstance();
            var booktrades = await client.BookTradesAsync(book: "btc_mxn", sort: "asc", limit: 10);
            Assert.IsNotNull(booktrades);
            Assert.IsTrue(booktrades.Count() > 0);
        }

        [Test]
        public async Task SocketClass_NoParams_Success()
        {
            var socket = Bitso.SocketClass.GetInstance();
            await socket.ConnectAsync();
            Assert.IsNotNull(socket.Result);
        }        
    }
}
