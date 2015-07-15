using System;
using System.Collections.Generic;

namespace UltraSixGenerator
{
    public class EasyLottery
    {
        public EasyLottery()
        {
            NumbersToBet = new List<int>();
            Results = new Dictionary<int, int>();
        }

        public IDictionary<int, int> Results;

        public int Concourse { get; set; }

        public DateTime Date { get; set; }

        public int FirstDozen { get; set; }

        public int SecondDozen { get; set; }

        public int ThirdDozen { get; set; }

        public int FourthDozen { get; set; }

        public int FifthDozen { get; set; }

        public int SixthDozen { get; set; }

        public int SeventhDozen { get; set; }

        public int EighthDozen { get; set; }

        public int NinethDozen { get; set; }

        public int TenthDozen { get; set; }

        public int EleventhDozen { get; set; }

        public int TwelfthDozen { get; set; }

        public int ThirteenthDozen { get; set; }

        public int FourteenthDozen { get; set; }

        public int FifteenthDozen { get; set; }        

        public IList<int> NumbersToBet { get; set; }
    }
}
