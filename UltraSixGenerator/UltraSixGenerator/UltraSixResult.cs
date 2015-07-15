using System;
using System.Collections.Generic;

namespace UltraSixGenerator
{
    public class UltraSixResult
    {
        public UltraSixResult()
        {
            NumbersToBet = new List<int>();
        }

        public int Concourse { get; set; }

        public DateTime Date { get; set; }

        public int FirstDozen { get; set; }
        
        public int SecondDozen { get; set; }

        public int ThirdDozen { get; set; }

        public int FourthDozen { get; set; }

        public int FifthDozen { get; set; }

        public int SixthDozen { get; set; }

        public IList<int> NumbersToBet { get; set; }
    }
}
