using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace UltraSixGenerator
{
    public class EasyThingGenerator : GeneratorBase
    {
        public override void PopulateManagerFromExcelUsingDb(string path)
        {
            var easyLotteryResults = new List<EasyLottery>();

            IList<int> dozens = new List<int>();

            var connectionString = string.Format(_connectionStringFormat, path);

            var dt = GetDataTable(connectionString);

            foreach (DataRow rowValue in dt.Rows)
            {
                if (rowValue[0] == DBNull.Value)
                    continue;

                var easyLottery = new EasyLottery
                {
                    Concourse = Convert.ToInt16(rowValue[0]),
                    Date = Convert.ToDateTime(rowValue[1]),
                    FirstDozen = Convert.ToInt16(rowValue[2]),
                    SecondDozen = Convert.ToInt16(rowValue[3]),
                    ThirdDozen = Convert.ToInt16(rowValue[4]),
                    FourthDozen = Convert.ToInt16(rowValue[5]),
                    FifthDozen = Convert.ToInt16(rowValue[6]),
                    SixthDozen = Convert.ToInt16(rowValue[7]),
                    SeventhDozen = Convert.ToInt16(rowValue[8]),
                    EighthDozen = Convert.ToInt16(rowValue[9]),
                    NinethDozen = Convert.ToInt16(rowValue[10]),
                    TenthDozen = Convert.ToInt16(rowValue[11]),
                    EleventhDozen = Convert.ToInt16(rowValue[12]),
                    TwelfthDozen = Convert.ToInt16(rowValue[13]),
                    ThirteenthDozen = Convert.ToInt16(rowValue[14]),
                    FourteenthDozen= Convert.ToInt16(rowValue[15]),
                    FifteenthDozen = Convert.ToInt16(rowValue[16]),
                };

                easyLotteryResults.Add(easyLottery);

                PopulateHowMany(easyLottery);
            }

            PopulateLastTenResults(easyLotteryResults);

            //Manager.Instance.Results = easyLotteryResults;
        }

        private void IncludeInDictionary(IDictionary<int, int> dictionary, int dozen)
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

        private void PopulateLastTenResults(IEnumerable<EasyLottery> easyLotteryResults)
        {
            var lastTenResults = easyLotteryResults.OrderByDescending(x => x.Date).Take(10);

            foreach (var easyLottery in lastTenResults)
            {
                Manager.Instance.IncludeDozenInLastResults(easyLottery.FirstDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.SecondDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.ThirdDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.FourthDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.FifthDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.SixthDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.SeventhDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.EighthDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.NinethDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.TenthDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.EleventhDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.TwelfthDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.ThirteenthDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.FourteenthDozen);
                Manager.Instance.IncludeDozenInLastResults(easyLottery.FifteenthDozen); 
            }
        }

        private static void PopulateHowMany(EasyLottery easyLottery)
        {
            Manager.Instance.IncludeDozenInHowMany(easyLottery.FirstDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.SecondDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.ThirdDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.FourthDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.FifthDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.SixthDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.SeventhDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.EighthDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.NinethDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.TenthDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.EleventhDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.TwelfthDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.ThirteenthDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.FourteenthDozen);
            Manager.Instance.IncludeDozenInHowMany(easyLottery.FifteenthDozen); 
        }
    }
}