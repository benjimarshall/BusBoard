using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusBoard.ConsoleApp
{
    class CliInterface
    {
        public static void RunUserCommandLoop(TflApi tflApi, PostcodeApi postcodeApi)
        {
            while (true)
            {
                Console.WriteLine("\n\"[Postcode]\" to list arrivals the two closest stops");
                Console.WriteLine("\"Quit\" to quit");

                Console.WriteLine("Please type an option: ");

                var input = Console.ReadLine();

                if (input == "Quit")
                {
                    break;
                }

                try
                {
                    Console.WriteLine(GetPredictionsForPostcode(tflApi, postcodeApi, input));
                }
                catch (Exception ex) when (ex is TflStopNotFoundException || ex is PostcodeNotFoundException)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static string GetPredictionsForPostcode(TflApi tflApi, PostcodeApi postcodeApi, string postcode)
        {
            var postcodeData = postcodeApi.GetPostcodeData(postcode);

            return GetPredictionsForLatLon(tflApi, postcodeData.latitude, postcodeData.longitude);
        }

        private static string GetPredictionsForLatLon(TflApi tflApi, double lat, double lon)
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
                result.Append($"Bus stop: {stopPoint.commonName}\n")
                      .Append(SummarisePredictions(arrivals))
                      .Append("\n\n");
            }

            return result.ToString();
        }

        private static string SummarisePredictions(IEnumerable<Prediction> predictions)
        {
            return string.Join(
                "\n",
                predictions.Take(5).Select(prediction => prediction.PredictionSummary)
            );
        }
    }
}
