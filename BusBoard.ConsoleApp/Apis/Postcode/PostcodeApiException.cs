using System;

namespace BusBoard.ConsoleApp
{
    class PostcodeApiException : Exception
    {
        public PostcodeApiException(string message) : base (message) { }
    }
}
