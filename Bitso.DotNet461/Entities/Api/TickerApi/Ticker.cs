namespace Bitso.Entities.Api.TickerApi
{
    public class Ticker
    {
        public string book { get; set; }
        public string volume { get; set; }
        public string high { get; set; }
        public string last { get; set; }
        public string low { get; set; }
        public string vwap { get; set; }
        public string ask { get; set; }
        public string bid { get; set; }
        public string created_at { get; set; }
    }
}

//Entity used for Ticker Api
