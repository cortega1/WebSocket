using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitso.Entities.WebSocketApi
{
    public class Subscription
    {
        public string action { get; set; }
        public string book { get; set; }
        public string type { get; set; }

        public Subscription()
        {
            action = "subscribe";
            book = "";
            type = "";
        }
    }
}