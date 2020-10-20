using System;

namespace BusBoard.ConsoleApp
{
    class StopPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public double distance { get; set; }
        public string id { get; set; }
        public string commonName { get; set; }

        public double LatLonDistance(double lat1, double lon1)
        {
            return Math.Sqrt(Math.Pow(lat1 - lat, 2) + Math.Pow(lon1 - lon, 2));
        }
    }
}
