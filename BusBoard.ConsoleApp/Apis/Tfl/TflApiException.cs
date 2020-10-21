using System;

namespace BusBoard.ConsoleApp
{
    class TflApiException : Exception
    {
        public TflApiException(string message) : base(message) { }
    }
}