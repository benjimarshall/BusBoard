using System;

namespace BusBoard.ConsoleApp
{
    class TflStopNotFoundException : Exception
    {
        public TflStopNotFoundException(string message) : base(message) { }
    }
}