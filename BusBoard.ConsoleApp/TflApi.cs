using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard.ConsoleApp
{
    class TflApi
    {
        private const string BaseUrl = "https://api.tfl.gov.uk/";

        readonly IRestClient client;

        public TflApi()
        {
            client = new RestClient(BaseUrl);
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var tflException = new Exception(message, response.ErrorException);
                throw tflException;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Stop was not found");
            }

            return response.Data;
        }

        public List<Prediction> GetArrivals(string stopId)
        {
            var request = new RestRequest($"StopPoint/{stopId}/Arrivals");

            return Execute<List<Prediction>>(request);
        }

        public List<Prediction> GetSortedArrivals(string stopId)
        {
            var arrivals = GetArrivals(stopId);

            arrivals.Sort((prediction1, prediction2) =>
                prediction1.timeToStation.CompareTo(prediction2.timeToStation)
            );

            return arrivals;
        }
    }
}
