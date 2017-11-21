using System.Collections.Generic;

namespace Bitso.Entities.Api.TradesApi
{
    public class TradesResponse
    {
        public bool success { get; set; }
        public ICollection<Trades> payload { get; set; }
    }
}
