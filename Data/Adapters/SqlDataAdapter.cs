using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Xml;

namespace Wduffy.Mvc.Data
{

    public class SqlDataAdapter : DataAdapter
    {

        protected string _borrowedConnectionKey;
        protected bool _hasBorrowedConnection;

#region Constructors

        /// <summary>
        /// Instantiates a DataAdapter object which implements a default connectionstring (DataBase)
        /// </summary>
        public SqlDataAdapter() : this("DataBase") { }

        /// <summary>
        /// Instantiates a DataAdapter object which implements the specified connectionstring
        /// </summary>
        /// <param name="connectionString">The name of a connection string that can be retrieved from the web.config ConnectionStrings section</param>        
        public SqlDataAdapter(string connectionString)
        {
            if (ConfigurationManager.ConnectionStrings[connectionString] == null)
                throw new ConfigurationErrorsException("The connectionstring '" + connectionString + "' could not be found in the web.config file.");
            
            _borrowedConnectionKey = "SqlDataAdapter_SqlCommand_" + connectionString;

            if (GetConnectionFromState() != null)
            {
                _cmd = GetConnectionFromState();
                _hasBorrowedConnection = true;
            }
            else
            {
                _cmd = new SqlCommand();
                _cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString);
                _cmd.Connection.Open();
                AddConnectionToState();
            }
        }

#endregion
#region IDisposable
        
        public override void Dispose()
        {
            if (!_hasBorrowedConnection)
            {
                DeleteConnectionFromState();
                CommandText = string.Empty;
                StoredProcedure = string.Empty;
                base.Dispose();
            }
        }

#endregion

#region Connection State

        protected SqlCommand GetConnectionFromState()
        {
            SqlCommand output = null;

            if (HttpContext.Current != null)
                output = (SqlCommand)HttpContext.Current.Items[_borrowedConnectionKey];
            
            return output;
        }

        protected void AddConnectionToState()
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items.Add(_borrowedConnectionKey, _cmd);
        }

        protected void DeleteConnectionFromState()
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items.Remove(_borrowedConnectionKey);
        }

#endregion

        /// <summary>
        /// Gets or sets the stored procedure that will be executed by the SqlCommand object
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string StoredProcedure
        {
            get { return _cmd.CommandText; }
            set
            {
                _cmd.Parameters.Clear();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = value;
            }
        }

        /// <summary>
        /// Executes the command and returns an object t SqlDataReader. The data reader is a read-only and forward-only cursor.
        /// </summary>
        /// <returns>An SqlDataReader that represents the data returned by the command</returns>
        /// <remarks></remarks>
        public new SqlDataReader ExecuteReader()
        {
            return (SqlDataReader)_cmd.ExecuteReader(CommandBehavior.Default);
        }

        /// <summary>
        /// Executes the command and returns an object t SqlDataReader. The data reader is a read-only and forward-only cursor.
        /// </summary>
        /// <param name="commandBehaviour"></param>
        /// <returns>An SqlDataReader that represents the data returned by the command</returns>
        /// <remarks></remarks>
        public new SqlDataReader ExecuteReader(CommandBehavior commandBehaviour)
        {
            return (SqlDataReader)_cmd.ExecuteReader(commandBehaviour);
        }

        /// <summary>
        /// Executes the command and returns an object of t XmlReader.
        /// </summary>
        /// <returns>An XmlReader object that represents the data returned by the SqlCommand object</returns>
        /// <remarks></remarks>
        public XmlReader ExecuteXmlReader()
        {
            return (_cmd as SqlCommand).ExecuteXmlReader();
        }

    }

}