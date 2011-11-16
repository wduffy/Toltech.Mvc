using System;
using System.Text;
using System.Web;
using System.IO;

namespace Toltech.Mvc.Tools
{

    public abstract class CsvBuilder
    {

        ///<summary>
        ///Sets the name of .csv file to be exported excluding the filetype extension
        ///</summary>
        ///<value>The name of the exported file as <see cref="System.String" /> excluding the filetype extension</value>
        ///<returns>The name of the exported file as <see cref="System.String" /> excluding the filetype extension</returns>
        ///<remarks></remarks>
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        private string _filename = "Export";

        ///<summary>
        ///Builds the CSV list
        ///</summary>
        ///<remarks>Implementation uses the template design pattern</remarks>
        public string Build()
        {
            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                Build(stream);
                reader.BaseStream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        ///<summary>
        ///Builds the CSV list
        ///</summary>
        ///<param name="stream">The System.IO.Stream which the csv will be written to</param>
        ///<remarks>Implementation uses the template design pattern</remarks>
        public void Build(Stream stream)
        {
            Validate();
            var writer = new StreamWriter(stream); // SteamWriter is not in a using because we don't want to close the supplied stream
            CreateCsv(writer);
            writer.Flush();
        }

        ///<summary>
        ///Performs error checking before processing the data to be exported
        ///</summary>
        ///<remarks></remarks>
        protected abstract void Validate();

        ///<summary>
        ///Build and write the csv file to the supplied stream
        ///</summary>
        ///<param name="csv"></param>
        ///<remarks></remarks>
        protected abstract void CreateCsv(StreamWriter writer);

    }

}