using System;

namespace BusBoard.Api.Tfl.Responses
{
    public class StopPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public double distance { get; set; }
        public string id { get; set; }
        public string commonName { get; set; }

        public double LatLonDistance(double latToCompare, double lonToCompare)
        {
            return Math.Sqrt(Math.Pow(latToCompare - lat, 2) + Math.Pow(lonToCompare - lon, 2));
        }
    }
}
