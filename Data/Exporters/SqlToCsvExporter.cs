using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Toltech.Data
{

    public class SqlToCsvExporter : CsvExporter
    {

        private SqlDataAdapter _data;
        public SqlDataAdapter Data
        {
            get
            {
                if (_data == null)
                    _data = new SqlDataAdapter();

                return _data;
            }
        }

        ///<summary>
        ///Sets the SQL statement which retrieves the data from the database used for construction of the .csv file
        ///</summary>
        ///<value>A SQL statement as <see cref="System.Text.StringBuilder" /></value>
        ///<returns>A SQL statement as <see cref="System.Text.StringBuilder" /></returns>
        ///<remarks></remarks>
        public StringBuilder SQL
        {
            get { return _sql; }
            set { _sql = value; }
        }
        private StringBuilder _sql = new StringBuilder();
        
        ///<summary>
        ///Add a new parameter to the SQL statement which retrieves the data from the database used for construction of the .csv file
        ///</summary>
        ///<param name="parameterName">The name of the SqlParameter as <see cref="System.String" /> including "@" character</param>
        ///<param name="sqlDbType">The parameter type as <see cref="System.Data.SqlDbType" /></param>
        ///<param name="parameterValue">The value to be passed in the parameter as <see cref="System.Object" /></param>
        ///<remarks></remarks>
        public void AddParameter(string parameterName, SqlDbType sqlDbType, object parameterValue)
        {
            _data.AddParameter(parameterName, sqlDbType, parameterValue, false);
        }
        private List<SqlParameter> _parameters = new List<SqlParameter>();

        ///<summary>
        ///Performs error checking on the current exporter object before processing the data to be exported
        ///</summary>
        ///<remarks></remarks>
        protected override void PerformErrorChecking()
        {
            if (this.SQL.Length == 0)
                throw new Exception("The property 'SQL' in '" + this.ToString() + "' cannot be NULL.");
        }

        ///<summary>
        ///Enumerate the objects and get the values stored in the requested properties
        ///</summary>
        ///<param name="csv"></param>
        ///<remarks></remarks>
        protected override void CreateCsv(StringBuilder csv)
        {

            using (_data)
            {
                _data.CommandText = this.SQL.ToString();

                SqlDataReader reader = _data.ExecuteReader();

                if (!reader.HasRows)
                    throw new Exception("<strong>Export failed:</strong> there are no records found that match your criteria.");

                // Create the header column
                for (int i = 0; i <= (reader.FieldCount - 1); i++)
                {
                    csv.Append(reader.GetName(i));
                    csv.Append(',');
                }

                csv.Remove(csv.Length - 1, 1);
                csv.AppendLine();

                // Create the data rows
                while (reader.Read())
                {
                    for (int i = 0; i <= (reader.FieldCount - 1); i++)
                    {
                        string value = reader.GetValue(i).ToString(); // All values are converted to string for use in a .csv file

                        if (value.Contains(",") || value.Contains(Environment.NewLine))
                        {
                            if (value.Contains("\""))
                                value = value.Replace("\"", "\"\""); // Replace " with ""

                            value = "\"" + value + "\"";
                        }

                        csv.Append(value);
                        csv.Append(',');
                    }

                    csv.Remove(csv.Length - 1, 1);
                    csv.AppendLine();
                }

            }

        }

    }

}