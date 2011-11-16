using System;
using System.Text;
using System.Web;

namespace Toltech.Data
{

    public abstract class CsvExporter
    {

        ///<summary>
        ///Sets the name of .csv file to be exported excluding the filetype prefix
        ///</summary>
        ///<value>The name of the exported file as <see cref="System.String" /> excluding the filetype prefix</value>
        ///<returns>The name of the exported file as <see cref="System.String" /> excluding the filetype prefix</returns>
        ///<remarks></remarks>
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        private string _filename = "export";

        ///<summary>
        ///Exports the generated .csv to the client
        ///</summary>
        ///<remarks>Implementation uses the template design pattern</remarks>
        public void Export()
        {
            StringBuilder csv = new StringBuilder(); // Create the stringbuilder to construct the csv

            this.PerformErrorChecking();
            this.CreateCsv(csv);
            this.SendCsvToClient(csv);
        }

        ///<summary>
        ///Performs error checking on the current exporter object before processing the data to be exported
        ///</summary>
        ///<remarks></remarks>
        protected abstract void PerformErrorChecking();

        ///<summary>
        ///Created the csv file to be exported to the client
        ///</summary>
        ///<param name="csv"></param>
        ///<remarks></remarks>
        protected abstract void CreateCsv(StringBuilder csv);

        ///<summary>
        ///Creates the response headers to force download of the csv file
        ///</summary>
        ///<remarks></remarks>
        private void SendCsvToClient(StringBuilder csv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + _filename + "(" + System.DateTime.Now.ToString("yyyy-MM-dd") + ").csv");
            HttpContext.Current.Response.Write(csv.ToString());
            HttpContext.Current.Response.End();
        }

    }

}