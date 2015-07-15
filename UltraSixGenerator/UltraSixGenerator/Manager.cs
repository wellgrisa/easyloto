using System;
using System.Linq;
using System.Collections.Generic;

namespace UltraSixGenerator
{
    public class Manager
    {
        private Manager()
        {
            HowMany = new Dictionary<int, int>();
            LastTenResults = new Dictionary<int, int>();
            Results = new List<UltraSixResult>();
        }

        private static Manager _instance;

        public static Manager Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (_instance == null)
                    {
                        _instance = new Manager();
                    }
                }
                return _instance;
            }
        }

        public IDictionary<int, int> HowMany { get; set; }

        public IDictionary<int, int> LastTenResults { get; set; }

        public IEnumerable<UltraSixResult> Results { get; set; }

        public void IncludeDozenInHowMany(int dozen)
        {
            IncludeInDictionary(HowMany, dozen);
        }

        public void IncludeDozenInLastResults(int dozen)
        {
            IncludeInDictionary(LastTenResults, dozen);
        }
        
        public void IncludeInDictionary(IDictionary<int, int> dictionary, int dozen)
        {
            if (dictionary.ContainsKey(dozen))
            {
                dictionary[dozen] = ++dictionary[dozen];
            }
            else
            {
                dictionary.Add(dozen, 1);
            }
        }
    }
}
