using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitso.DotNet461.Entities
{
    abstract class SocketResponse
    {
        public string type { get; set; }
        public string book { get; set; }
        public Object payload { get; set; }
    }
}