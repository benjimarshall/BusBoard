using System;

namespace BusBoard.Api.Postcode
{
    public class PostcodeApiException : Exception
    {
        public PostcodeApiException(string message) : base (message) { }
    }
}
