using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
    class StopPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public double distance { get; set; }
        public string id { get; set; }
        public string commonName { get; set; }
    }
}
