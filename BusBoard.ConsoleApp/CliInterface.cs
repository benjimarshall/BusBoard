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

        public static string SummarisePredictions(IEnumerable<Prediction> predictions, int count = 5)
        {
            return string.Join(
                "\n",
                predictions.Take(count).Select(prediction => prediction.PredictionSummary)
            );
        }
    }
}