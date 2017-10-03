using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitso.DotNet461.Entities
{
    public class OrderBook
    {
        public bool success { get; set; }
        public OrderBookPayload payload { get; set; }
    }

    public class Asks
    {
        public string book { get; set; }
        public string price { get; set; }
        public string amount { get; set; }
    }

    public class Bids
    {
        public string book { get; set; }
        public string price { get; set; }
        public string amount { get; set; }
    }

    public class OrderBookPayload
    {
        public ICollection<Asks> asks { get; set; }
        public ICollection<Bids> bids { get; set; }
        public string updated_at { get; set; }
        public string sequence { get; set; }
    }
}
