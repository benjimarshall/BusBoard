using System;

namespace BusBoard.Api.Tfl
{
    public class TflApiException : Exception
    {
        public TflApiException(string message) : base(message) { }
    }
}