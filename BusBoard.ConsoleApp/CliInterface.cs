﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                else
                {
                    try
                    {
                        var predictions = tflApi.GetSortedArrivals(input);

                        var iterations = Math.Max(predictions.Count, 5);
                        for (var i = 0; i < iterations; i++)
                        {
                            Console.WriteLine(predictions[i].PredictionSummary);
                        }
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}