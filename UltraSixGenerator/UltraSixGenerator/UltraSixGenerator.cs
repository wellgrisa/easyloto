using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace UltraSixGenerator
{
    public class UltraSixGenerator : GeneratorBase
    {
        public UltraSixResult Generate()
        {
            return Generate(6);
        }

        public UltraSixResult Generate(int countOfNumbers)
        {
            var ultraSixResult = new UltraSixResult();

            var random = new Random();

            for (int i = 0; i < countOfNumbers; i++)
                ultraSixResult.NumbersToBet.Add(random.Next(0, 60));

            return ultraSixResult;
        }        

        public override void PopulateManagerFromExcelUsingDb(string path)
        {
            var ultraSixResults = new List<UltraSixResult>();

            IList<int> dozens = new List<int>();

            var connectionString = string.Format(_connectionStringFormat, path);

            var dt = GetDataTable(connectionString);

            foreach (DataRow rowValue in dt.Rows)
            {
                if (rowValue[0] == DBNull.Value)
                    continue;

                var ultraSixResult = new UltraSixResult
                {
                    Concourse = Convert.ToInt16(rowValue[0]),
                    Date = Convert.ToDateTime(rowValue[1]),
                    FirstDozen = Convert.ToInt16(rowValue[2]),
                    SecondDozen = Convert.ToInt16(rowValue[3]),
                    ThirdDozen = Convert.ToInt16(rowValue[4]),
                    FourthDozen = Convert.ToInt16(rowValue[5]),
                    FifthDozen = Convert.ToInt16(rowValue[6]),
                    SixthDozen = Convert.ToInt16(rowValue[7])
                };

                ultraSixResults.Add(ultraSixResult);
                
                PopulateHowMany(ultraSixResult);
            }

            PopulateLastTenResults(ultraSixResults);

            Manager.Instance.Results = ultraSixResults;
        }
        
        private void PopulateLastTenResults(IEnumerable<UltraSixResult> ultraSixResults)
        {
            var lastTenResults = ultraSixResults.OrderByDescending(x => x.Date).Take(10);

            foreach (var ultraSixResult in lastTenResults)
            {
                Manager.Instance.IncludeDozenInLastResults(ultraSixResult.FirstDozen);
                Manager.Instance.IncludeDozenInLastResults(ultraSixResult.SecondDozen);
                Manager.Instance.IncludeDozenInLastResults(ultraSixResult.ThirdDozen);
                Manager.Instance.IncludeDozenInLastResults(ultraSixResult.FourthDozen);
                Manager.Instance.IncludeDozenInLastResults(ultraSixResult.FifthDozen);
                Manager.Instance.IncludeDozenInLastResults(ultraSixResult.SixthDozen);   
            }
        }

        private static void PopulateHowMany(UltraSixResult ultraSixResult)
        {
            Manager.Instance.IncludeDozenInHowMany(ultraSixResult.FirstDozen);
            Manager.Instance.IncludeDozenInHowMany(ultraSixResult.SecondDozen);
            Manager.Instance.IncludeDozenInHowMany(ultraSixResult.ThirdDozen);
            Manager.Instance.IncludeDozenInHowMany(ultraSixResult.FourthDozen);
            Manager.Instance.IncludeDozenInHowMany(ultraSixResult.FifthDozen);
            Manager.Instance.IncludeDozenInHowMany(ultraSixResult.SixthDozen);
        }
    }
}
