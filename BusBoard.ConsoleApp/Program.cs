using System.Net;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var tflApi = new TflApi();

            CliInterface.RunUserCommandLoop(tflApi);
        }
    }
}
