using System;
using System.Net;
using BusBoard.Api.Postcode;
using BusBoard.Api.Tfl;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var tflApi = new TflApi();
            var postcodeApi = new PostcodeApi();

            CliInterface.RunUserCommandLoop(tflApi, postcodeApi);
        }
    }
}
