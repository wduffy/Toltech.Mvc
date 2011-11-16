using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Toltech.Data
{

    public class ExcelDataAdapter : DataAdapter
    {

        private string _temporaryFile;

#region " Constructors "

        /// <summary>
        /// Instantiates a DataAdapter object which implements the specified Excel document
        /// </summary>
        /// <param name="connectionString">The name of the excel file that wil be connected to.</param>
        public ExcelDataAdapter(System.Web.HttpPostedFile postedfile)
        {
            if (!postedfile.FileName.EndsWith(".xls"))
                throw new ArgumentException("Only Microsoft Excel spreadsheets (.xls) are supported by the Excel Data Adapter.");

            _temporaryFile = Path.GetTempFileName();
            postedfile.SaveAs(_temporaryFile);

            SetupConnection(_temporaryFile);
        }

        /// <summary>
        /// Instantiates a DataAdapter object which implements the specified Excel document
        /// </summary>
        /// <param name="connectionString">The name of the excel file that wil be connected to.</param>
        public ExcelDataAdapter(string excelFile)
        {
            if (!File.Exists(excelFile))
                throw new FileNotFoundException("The excel file '" + excelFile + "' could not be found.");

            SetupConnection(excelFile);
        }

#endregion

        private void SetupConnection(string excelFile)
        {
            _cmd = new OleDbCommand();
            _cmd.Connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFile + ";Extended Properties='Excel 8.0;HDR=YES;'");
            _cmd.Connection.Open();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (!string.IsNullOrEmpty(_temporaryFile) && File.Exists(_temporaryFile))
                File.Delete(_temporaryFile);
        }

        /// <summary>
        /// Executes the command and returns an object type SqlDataReader. The data reader is a read-only and forward-only cursor.
        /// </summary>
        /// <returns>An SqlDataReader that represents the data returned by the command</returns>
        /// <remarks></remarks>
        public new OleDbDataReader ExecuteReader()
        {
            return (OleDbDataReader)_cmd.ExecuteReader(CommandBehavior.Default);
        }

        /// <summary>
        /// Executes the command and returns an object type SqlDataReader. The data reader is a read-only and forward-only cursor.
        /// </summary>
        /// <param name="commandBehaviour"></param>
        /// <returns>An SqlDataReader that represents the data returned by the command</returns>
        /// <remarks></remarks>
        public new OleDbDataReader ExecuteReader(CommandBehavior commandBehaviour)
        {
            return (OleDbDataReader)_cmd.ExecuteReader(commandBehaviour);
        }

    }

}