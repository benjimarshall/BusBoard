using System;

namespace BusBoard.ConsoleApp
{
    class PredictionTiming
    {
        public DateTime countdownServerAdjustment { get; set; }
        public DateTime source { get; set; }
        public DateTime insert { get; set; }
        public DateTime read { get; set; }
        public DateTime sent { get; set; }
        public DateTime received { get; set; }

    }
}
