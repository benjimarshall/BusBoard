using System;

namespace BusBoard.ConsoleApp
{
    class PostcodeNotFoundException : Exception
    {
        public PostcodeNotFoundException(string message) : base (message) { }
    }
}
