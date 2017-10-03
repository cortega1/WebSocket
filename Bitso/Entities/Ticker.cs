using System;
using System.Collections.Generic;
using System.Text;

namespace Bitso.Entities
{
    public class Ticker
    {
        public string high { get; set; }
        public string last { get; set; }
        public string created_at { get; set; }
        public string book { get; set; }
        public string volume { get; set; }
        public string vwap { get; set; }
        public string low { get; set; }
        public string ask { get; set; }
        public string bid { get; set; }
    }

    public class TickerResponse
    {
        public bool success { get; set; }
        public Ticker payload { get; set; }
    }
}
