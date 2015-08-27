using CsQuery;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using UltraSixGenerator;

namespace EasyLottery.WPF.ViewModel
{
    [ImplementPropertyChanged]
    public class MainWindowViewModel
    {
        #region Attributes
        private const int _hot = 10;

        private bool _refreshing = false;
        #endregion

        #region Properties

        public ICommand RefreshCommand { get; set; }

        public int Quantity { get; set; }

        public ObservableCollection<ResultViewModel> Results { get; set; }

        public ObservableCollection<ResultViewModel> HotResults { get; set; }

        public ObservableCollection<ResultViewModel> ColdResults { get; set; }

        #endregion


        public MainWindowViewModel()
        {
            Quantity = _hot;

            RefreshCommand = new RelayCommand(RefreshExecute, x => !_refreshing);

            Results = new ObservableCollection<ResultViewModel>(new List<ResultViewModel>());

            RefreshExecute();
        }

        private  IEnumerable<ResultViewModel> BuildResultsViewModel(IEnumerable<Result> webResults)
        {
            var results = webResults.Take(Quantity).SelectMany(x => x.Dozens).GroupBy(x => x);

            foreach (var result in results)
            {
                yield return new ResultViewModel
                {
                    Number = Convert.ToInt32(result.Key),
                    Quantity = result.Count()
                };
            }
        }

        private IEnumerable<Result> GenerateResultsByWeb()
        {
            var dom = CQ.CreateFromUrl("http://www.ferramentaslotofacil.com.br/lotofacil.php");

            var metadata = dom["*[style='width:450px; float:left;']"];

            var regex = new Regex(@"[ ]{2,}");

            var councourseNumber = 1253;

            foreach (var data in metadata)
            {
                var result = new Result { ConcourseNumber = councourseNumber };

                foreach (var childElement in data.ChildElements)
                {
                    var dozensArray = regex
                        .Replace(childElement.InnerText, @" ")
                        .Replace("\n", string.Empty)
                        .Trim()
                        .Split(' ');

                    foreach (var dozen in dozensArray.Where(x => !string.IsNullOrEmpty(x)))
                    {
                        result.Dozens.Add(dozen);
                    }
                }

                councourseNumber--;

                yield return result;
            }
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

        private void RefreshExecute(object obj = null)
        {
            var webResults = GenerateResultsByWeb();

            _refreshing = true;

            Results.Clear();

            foreach (var item in BuildResultsViewModel(webResults).OrderByDescending(x => x.Quantity))
            {
                Results.Add(item);
            }

            _refreshing = false;
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

    public class Result
    {
        public int ConcourseNumber { get; set; }

        public IList<string> Dozens { get; set; }

        public Result()
        {
            Dozens = new List<string>();
        }
    }

    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
