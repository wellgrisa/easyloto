using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraSixGenerator;

namespace EasyLottery.WPF.ViewModel
{
    public class MainWindowViewModel
    {
        public ObservableCollection<ResultViewModel> HotResults { get; set; }

        public ObservableCollection<ResultViewModel> ColdResults { get; set; }

        public MainWindowViewModel()
        {
            var results = GenerateResults().ToList();            
            
            ColdResults = new ObservableCollection<ResultViewModel>(results);

            results.Reverse(); 

            HotResults = new ObservableCollection<ResultViewModel>(results);
        }

        private IEnumerable<ResultViewModel> GenerateResults()
        {
            EasyThingGenerator generator = new EasyThingGenerator();

            generator.PopulateManagerFromExcelUsingDb("C:\\easyloto.xlsx");

            var orderedHowManyResults = Manager.Instance.HowMany.OrderBy(x => x.Value);

            foreach (var result in orderedHowManyResults)
            {
                yield return new ResultViewModel { Number = result.Key, Quantity = result.Value };
            }
        }
    }

    public class ResultViewModel
    {
        public int Quantity { get; set; }

        public int Number { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - Quantity - {1}", Number, Quantity);
        }
    }
}
