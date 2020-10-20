using System.Net;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    class PostcodeApi
    {
        private const string BaseUrl = "https://api.postcodes.io/";

        private readonly IRestClient client;

        public PostcodeApi()
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
                throw new PostcodeNotFoundException("Postcode was not found");
            }

            return response.Data;
        }

        public PostcodeData GetPostcodeData(string postcode)
        {
            var request = new RestRequest($"postcodes/{postcode}");

            return Execute<PostcodeWrapper>(request).result;
        }
    }
}
