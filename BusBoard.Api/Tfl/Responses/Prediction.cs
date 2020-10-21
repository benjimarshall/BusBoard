using System;

namespace BusBoard.Api.Tfl.Responses
{
    public class Prediction
    {
        public string id { get; set; }
        public int operationType { get; set; }
        public string vehicleId { get; set; }
        public string naptanId { get; set; }
        public string stationName { get; set; }
        public string lineId { get; set; }
        public string lineName { get; set; }
        public string platformName { get; set; }
        public string direction { get; set; }
        public string bearing { get; set; }
        public string destinationNaptanId { get; set; }
        public string destinationName { get; set; }
        public DateTime timestamp { get; set; }
        public int timeToStation { get; set; }
        public string currentLocation { get; set; }
        public string towards { get; set; }
        public DateTime expectedArrival { get; set; }
        public string timeToLive { get; set; }
        public string modeName { get; set; }
        public PredictionTiming timing { get; set; }

        public int minutesToStation => timeToStation / 60;

        public string PredictionSummary =>
            $"{lineName} bus to {destinationName} will arrive in {minutesToStation} minutes";
    }
}
