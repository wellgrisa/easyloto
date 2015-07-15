using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;

namespace UltraSixGenerator.Tests
{
    [TestFixture]
    public class GeneratorTests
    {

        public string YearLeft { get; set; }

        [Test]
        public void Generate_Six_Numbers()
        {
            Generator generator = new Generator();

            UltraSixResult result = generator.Generate(6);

            Assert.AreEqual(6, result.NumbersToBet.Count);
        }

        [Test]
        public void Read_From_Excel_File_And_Get_The_Ten_Last_Rows()
        {
            Generator generator = new Generator();

            generator.PopulateManagerFromExcelUsingDb("C:\\UltraSix.xlsx");

            var ordernede = Manager.Instance.LastTenResults.OrderBy(x => x.Key);


            var howmany = Manager.Instance.HowMany.OrderBy(x => x.Value);
        }

        [Test]
        public void Read_From_Excel_File_And_Get_The_Ten_Last_Rows_Easy_Loto()
        {
            Generator generator = new Generator();

            generator.PopulateManagerFromExcelUsingDb("C:\\UltraSix.xlsx");

            var ordernede = Manager.Instance.LastTenResults.OrderBy(x => x.Key);


            var howmany = Manager.Instance.HowMany.OrderBy(x => x.Value);
        }

        [Test]
        public void Blabla()
        {


            var a = GetPropertyName(() => YearLeft);
        }

        public string GetPropertyName<T>(Expression<Func<T>> lambda)
        {
            var memberExpression = (MemberExpression)((lambda.Body is UnaryExpression)
                   ? ((UnaryExpression)lambda.Body).Operand : lambda.Body);

            var a = (memberExpression.Member as PropertyInfo).GetSetMethod();

            a.Invoke(this, new object[] { "test" });
            
            return memberExpression.Member.Name;
        }
    }
}
