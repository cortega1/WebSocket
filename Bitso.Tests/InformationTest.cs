using System;
using NUnit.Framework;

namespace Bitso.Tests
{
    [TestFixture]
    public class InformationTest
    {
        [Test]
        public void GetBookList_NoParams_Success() {
            var client = new Bitso.Information();
            var books = client.ListBooks().Result;

            Assert.IsNotNull(books);
            Assert.IsTrue(books.Count > 0);
        }
    }
}
