using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusBoard.ConsoleApp
{
    class CliInterface
    {
        public static void RunUserCommandLoop(TflApi tflApi)
        {
            while (true)
            {
                Console.WriteLine("\n\"[Bus stop code]\" to list arrivals at that stop");
                Console.WriteLine("\"Quit\" to quit");

                Console.WriteLine("Please type an option: ");

                var input = Console.ReadLine();

                if (input == "Quit")
                {
                    break;
                }

                try
                {
                    var predictions = tflApi.GetSortedArrivals(input);

                    Console.WriteLine(SummarisePredictions(predictions));
                }
                catch (TflStopNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static string GetPredictionsForLatLon(TflApi tflApi, double lat, double lon, int count = 2)
        {
            var stopPoints = tflApi.GetStopPointsAtLocation(lat, lon);

            if (!stopPoints.Any())
            {
                return "No bus stops near this location.";
            }

            var result = new StringBuilder();
            foreach (var stopPoint in stopPoints.Take(2))
            {
                var arrivals = tflApi.GetSortedArrivals(stopPoint.id);
                result.Append($"Bus stop: {stopPoint.commonName}")
                      .Append(SummarisePredictions(arrivals))
                      .Append("\n");
            }

            return result.ToString();
        }

        private static string SummarisePredictions(IEnumerable<Prediction> predictions)
        {
            return string.Join(
                "\n",
                predictions.Take(count).Select(prediction => prediction.PredictionSummary)
            );
        }
    }
}
