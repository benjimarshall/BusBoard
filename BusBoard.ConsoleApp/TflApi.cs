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
                throw new ArgumentException("Stop was not found");
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
            var arrivals = GetArrivals(stopId);

            return arrivals.OrderBy(prediction => prediction.timeToStation).ToList();
        }
    }
}
