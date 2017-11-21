using System.Collections.Generic;

namespace Bitso.Entities.Api.AvailableBooksApi
{
    public class BooksResponse
    {
        public bool success { get; set; }
        public ICollection<Book> payload { get; set; }
    }
}

// Used for available books api
