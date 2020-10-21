using System.Collections.Generic;
using System.Linq;
using System.Net;
using BusBoard.Api.Tfl.Responses;
using RestSharp;

namespace BusBoard.Api.Tfl
{
    public class TflApi
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
                throw new TflApiException("Stop was not found");
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new TflApiException("Location requested is too far from London");
            }
            else if (response.Data == null)
            {
                // The data didn't deserialise, which could be caused by anything from a bad network
                // to TFL changing their API in some breaking way.
                throw new TflApiException("There was an error retrieving data from TfL");
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

            var stopPoints = Execute<StopPointsWrapper>(request).stopPoints;

            // Stops roughly within 500m of given point. Due to caching done by TFL, the distances they use are only
            // accurate to ~100m due to heavy rounding of lat and lon parameters.
            // Therefore, recalculate distances and update stopPoints to have greater precision
            stopPoints.ForEach(stopPoint =>
                stopPoint.distance = stopPoint.LatLonDistance(lat, lon)
            );

            return stopPoints.OrderBy(stopPoint => stopPoint.distance);
        }
    }
}
