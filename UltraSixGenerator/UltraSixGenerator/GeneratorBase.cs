using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace UltraSixGenerator
{
    public abstract class GeneratorBase
    {
        protected string _connectionStringFormat = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES';";

        public abstract void PopulateManagerFromExcelUsingDb(string path);

        protected DataTable GetDataTable(string connectionString)
        {
            var dt = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                DataTable ExcelSheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                string SpreadSheetName = ExcelSheets.Rows[0]["TABLE_NAME"].ToString();

                var query = "SELECT * FROM [" + SpreadSheetName + "]";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    using (OleDbDataReader rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                        return dt;
                    }
                }
            }
        }
    }    
}
