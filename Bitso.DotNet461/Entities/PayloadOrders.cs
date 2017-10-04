using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitso.DotNet461.Entities
{
    public class PayloadOrders
    {
        public BidsPayload bids { get; set; }
        public AsksPayload asks { get; set; }
    }

    public class BidsPayload
    {
        public string r { get; set; }
        public string a { get; set; }
        public string v { get; set; }
        public int t { get; set; }
        public string d { get; set; }
    }

    public class AsksPayload
    {
        public string r { get; set; }
        public string a { get; set; }
        public string v { get; set; }
        public int t { get; set; }
        public string d { get; set; }
    }
}