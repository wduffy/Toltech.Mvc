using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Xml;

namespace Wduffy.Mvc.Data
{

    public abstract class DataAdapter : IDisposable
    {
    
        protected DbCommand _cmd = null;

        public DbParameterCollection Parameters
        {
            get
            {
                return _cmd.Parameters;
            }
        }

        /// <summary>
        /// Gets or sets the statement that will be executed by the Command object
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string CommandText
        {
            get { return _cmd.CommandText; }
            set 
            {
                _cmd.Parameters.Clear();
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = value;
            }
        }

        /// <summary>
        /// Begins a new transaction
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public void TransactionBeing()
        {
            _cmd.Transaction = _cmd.Connection.BeginTransaction();
        }

        /// <summary>
        /// Commits a transaction and disposes of it
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public void TransactionCommit()
        {
            if (_cmd.Transaction == null)
                throw new Exception("There is no transaction to commit.");

            _cmd.Transaction.Commit();
            _cmd.Transaction = null;
        }

        /// <summary>
        /// Rolls back a transaction and disposes of it
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public void TransactionRollBack()
        {
            if (_cmd.Transaction == null)
                throw new Exception("There is no transaction to rollback.");

            _cmd.Transaction.Rollback();
            _cmd.Transaction = null;
        }

        /// <summary>
        /// Executes the command and returns an object t DbDataReader. The data reader is a read-only and forward-only cursor.
        /// </summary>
        /// <returns>A DbDataReader that represents the data returned by the command</returns>
        /// <remarks></remarks>
        public virtual DbDataReader ExecuteReader()
        {
            return this.ExecuteReader(CommandBehavior.Default);
        }

        /// <summary>
        /// Executes the command and returns an object t DbDataReader. The data reader is a read-only and forward-only cursor.
        /// </summary>
        /// <param name="commandBehaviour"></param>
        /// <returns>A DbDataReader that represents the data returned by the command</returns>
        /// <remarks></remarks>
        public virtual DbDataReader ExecuteReader(CommandBehavior commandBehaviour)
        {
            return _cmd.ExecuteReader(commandBehaviour);
        }

        /// <summary>
        /// Executes the command and returns first columnn of the first row in the form of a generic object
        /// </summary>
        /// <returns>The first column in the first row as a generic object</returns>
        /// <remarks></remarks>
        public object ExecuteScalar()
        {
            return _cmd.ExecuteScalar();
        }

        /// <summary>
        /// Executes the command and returns the number of rows affected
        /// </summary>
        /// <returns>Number of rows affected as System.Int32</returns>
        /// <remarks></remarks>
        public int ExecuteNonQuery()
        {
            return _cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets a DataSet that represents the data returned by the OleDbCommand object
        /// </summary>
        /// <returns>A DataSet that represents the data returned by the SqlCommand object</returns>
        /// <remarks></remarks>
        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            DbDataAdapter da = null;

            if (_cmd.GetType() == typeof(System.Data.SqlClient.SqlDataAdapter))
                da = new System.Data.SqlClient.SqlDataAdapter();
            else if (_cmd.GetType() == typeof(System.Data.OleDb.OleDbDataAdapter))
                da = new System.Data.OleDb.OleDbDataAdapter();

            if (da != null)
            {
                da.Fill(dt);
                da.Dispose();
            }

            return dt;
        }

#region Static Methods

        /// <summary>
        /// Checks to see if a value is the default value as handled by this domain
        /// </summary>
        /// <param name="value">The value to be assessed</param>
        /// <returns></returns>
        public static bool IsDefaultValue(object value)
        {
            bool output = false;

            // Assess the filter value for NULL depending on it's t
            switch (value.GetType().FullName)
            {
                case "System.Boolean":
                    output = false;
                    break;
                case "System.Byte":
                    output = value.Equals(byte.MinValue);
                    break;
                case "System.Char":
                    output = value.Equals(char.MinValue);
                    break;
                case "System.DateTime":
                    output = value.Equals(DateTime.MinValue);
                    break;
                case "System.Decimal":
                    output = value.Equals(decimal.MinValue);
                    break;
                case "System.Double":
                    output = value.Equals(double.MinValue);
                    break;
                case "System.Guid":
                    output = value.Equals(Guid.Empty);
                    break;
                case "System.Int16":
                    output = value.Equals(short.MinValue);
                    break;
                case "System.Int32":
                    output = value.Equals(int.MinValue);
                    break;
                case "System.Int64":
                    output = value.Equals(long.MinValue);
                    break;
                case "System.SByte":
                    output = value.Equals(sbyte.MinValue);
                    break;
                case "System.Single":
                    output = value.Equals(float.MinValue);
                    break;
                case "System.String":
                    output = value.Equals(string.Empty);
                    break;
                case "System.UInt16":
                    output = value.Equals(ushort.MinValue);
                    break;
                case "System.UInt32":
                    output = value.Equals(uint.MinValue);
                    break;
                case "System.UInt64":
                    output = value.Equals(ulong.MinValue);
                    break;
                default:
                    output = value == null;
                    break;
            }

            return output;
        }

#endregion
#region " IDisposable Members "

        private bool _disposed = false; // To detect redundant calls

        // IDisposable
        public virtual void Dispose()
        {
            this.Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
 	         if (!_disposed)
             {
                if (disposing)
                {
                    // Free unmanaged resources when explicitly called
                    _cmd.Connection.Close();
                    _cmd.Connection = null;
                    _cmd.Dispose();
                }

                // Free shared unmanaged resources
                // Set large fields to null.
                _cmd = null;
             }

            _disposed = true;
        }

#endregion

    }

}