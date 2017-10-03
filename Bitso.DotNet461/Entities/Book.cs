using System;
using System.Collections.Generic;
using System.Text;

namespace Bitso.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Book
    {
        /// <summary>
        /// 
        /// </summary>
        public string book { get; set; }
        public string minimum_amount { get; set; }
        public string maximum_amount { get; set; }
        public string minimum_price { get; set; }
        public string maximum_price { get; set; }
        public string minimum_value { get; set; }
        public string maximum_value { get; set; }
    }

    public class BooksResponse
    {
        public bool success { get; set; }
        public ICollection<Book> payload { get; set; }
    }
}
