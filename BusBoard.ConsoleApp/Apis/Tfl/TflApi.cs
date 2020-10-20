using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    class TflApi
    {
        private const string BaseUrl = "https://api.tfl.gov.uk/";

        private readonly IRestClient client;

        public TflApi()
        {
            client = new RestClient(BaseUrl);
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new TflStopNotFoundException("Stop was not found");
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new TflStopNotFoundException("Location requested is too far from London");
            }

            return response.Data;
        }

        public IEnumerable<Prediction> GetArrivals(string stopId)
        {
            var request = new RestRequest($"StopPoint/{stopId}/Arrivals");

            return Execute<List<Prediction>>(request);
        }

        public IEnumerable<Prediction> GetSortedArrivals(string stopId)
        {
            return GetArrivals(stopId).OrderBy(prediction => prediction.timeToStation);
        }

        public IEnumerable<StopPoint> GetStopPointsAtLocation(double lat, double lon)
        {
            var request = new RestRequest($"StopPoint?" +
                                          $"radius=500" +
                                          $"&stopTypes=NaptanBusCoachStation,NaptanPublicBusCoachTram" +
                                          $"&lat={lat}&lon={lon}"
                                          );

            // Stops roughly within 500m of given point. Due to caching done by TFL, the distances they use are only
            // accurate to ~100m due to heavy rounding of lat and lon parameters
            var stopPoints = Execute<StopPointsWrapper>(request).stopPoints;

            // Recalculate distances and update stopPoints to have greater precision
            stopPoints.ForEach(stopPoint =>
                stopPoint.distance = stopPoint.LatLonDistance(lat, lon)
            );

            // Sort by updated distances
            return stopPoints.OrderBy(stopPoint => stopPoint.distance);
        }
    }
}
