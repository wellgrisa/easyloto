using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraSixGenerator;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            EasyThingGenerator generator = new EasyThingGenerator();

            generator.PopulateManagerFromExcelUsingDb("C:\\easyloto.xlsx");

            //var ordernede = Manager.Instance.LastTenResults.OrderBy(x => x.Key);


            var howmany = Manager.Instance.HowMany.OrderBy(x => x.Value);

            var abc = Manager.Instance.HowMany.OrderBy(x => x.Key); 

            
        }
    }
}
