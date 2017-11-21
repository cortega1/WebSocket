using System.Collections.Generic;

namespace Bitso.Entities.Api.OrderBookApi
{
    public class OrderBookPayload
    {
        public ICollection<Asks> asks { get; set; }
        public ICollection<Bids> bids { get; set; }
        public string updated_at { get; set; }
        public string sequence { get; set; }
    }
}
