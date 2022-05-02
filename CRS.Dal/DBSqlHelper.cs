using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRS.Dal
{
    public class DBSqlHelper
    {
        private string connStr = AppConfiguration.ConnectionString.ToString();

        public bool ExecuteNonQueryDB(string Query)
        {
            if (!string.IsNullOrEmpty(Query))
            {
                SqlConnection m_Scon = new SqlConnection(connStr);
                SqlCommand m_Scmd = new SqlCommand(Query, m_Scon);
                m_Scon.Close();
                m_Scon.Open();
                int m_Sdr = m_Scmd.ExecuteNonQuery();

                if (m_Sdr > 0)
                {
                    if (m_Scon.State == ConnectionState.Open)
                    {
                        m_Scon.Close();
                    }
                    return true;
                }
                else
                {
                    if (m_Scon.State == ConnectionState.Open)
                    {
                        m_Scon.Close();
                    }
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        ///
        /// Used to execute query using ExecuteReader() Method
        ///
        ///
        /// IDataReader
        public SqlDataReader ExecuteReaderDB(string Query)
        {
            SqlConnection m_Scon = new SqlConnection(connStr);
            SqlCommand m_Scmd = new SqlCommand(Query, m_Scon);
            m_Scon.Close();
            m_Scon.Open();
            SqlDataReader m_Sdr = m_Scmd.ExecuteReader(CommandBehavior.CloseConnection);
            return m_Sdr;
        }

        ///
        /// Used to execute query using ExecuteDataSet() Method
        ///
        ///
        /// DataSet
        public DataSet ExecuteDataSetDB(string Query)
        {
            if (!string.IsNullOrEmpty(Query))
            {
                SqlConnection m_Scon = new SqlConnection(connStr);
                SqlCommand m_Scmd = new SqlCommand(Query, m_Scon);
                DataSet m_DSObj = new DataSet();
                SqlDataAdapter adr = new SqlDataAdapter(m_Scmd);
                adr.Fill(m_DSObj);

                if (m_Scon.State == ConnectionState.Open)
                {
                    m_Scon.Close();
                }
                return m_DSObj;
            }
            else
            {
                return null;
            }
        }

        ///
        ///Used to execute the stored procedure and return DataSet
        ///
        ///
        ///
        ///DataSet
        public DataSet ExecuteDataSetSPDB(string ProcedureName, SqlParameter[] Parameters)
        {
            if (!string.IsNullOrEmpty(ProcedureName))
            {
                SqlConnection m_Scon = new SqlConnection(connStr);
                SqlCommand m_Scmd = new SqlCommand(ProcedureName, m_Scon);
                m_Scmd.CommandType = CommandType.StoredProcedure;
                DataSet m_DSObj = new DataSet();

                if (Parameters != null)
                {
                    foreach (SqlParameter para in Parameters)
                    {
                        m_Scmd.Parameters.Add(para);
                    }
                }

                SqlDataAdapter adr = new SqlDataAdapter(m_Scmd);
                adr.Fill(m_DSObj);
                
                if (m_Scon.State == ConnectionState.Open)
                {
                    m_Scon.Close();
                }
                return m_DSObj;
            }
            else
            {
                return null;
            }
        }

        ///
        /// Used to execute the stored procedure and return IDataReader
        ///
        ///
        ///
        /// IDataReader
        public SqlDataReader ExecuteReaderSPDB(string ProcedureName, SqlParameter[] Parameters)
        {
            if (!string.IsNullOrEmpty(ProcedureName))
            {
                SqlConnection m_Scon = new SqlConnection(connStr);
                SqlCommand m_Scmd = new SqlCommand(ProcedureName, m_Scon);
                m_Scmd.CommandType = CommandType.StoredProcedure;
                m_Scmd.CommandTimeout = 1800;
                m_Scon.Close();
                m_Scon.Open();

                if (Parameters != null)
                {
                    foreach (SqlParameter para in Parameters)
                    {
                        m_Scmd.Parameters.Add(para);
                    }
                }

                SqlDataReader m_Sdr = m_Scmd.ExecuteReader(CommandBehavior.CloseConnection);
                
                return m_Sdr;
            }
            else
            {
                return null;
            }
        }

        ///
        /// Used to execute the stored procedure using executenonquery
        ///
        ///
        ///
        /// Boolean
        public bool ExecuteNonQuerySPDB(string ProcedureName, SqlParameter[] Parameters)
        {
            if (!string.IsNullOrEmpty(ProcedureName))
            {
                SqlConnection m_Scon = new SqlConnection(connStr);
                SqlCommand m_Scmd = new SqlCommand(ProcedureName, m_Scon);
                m_Scmd.CommandType = CommandType.StoredProcedure;
                m_Scon.Close();
                m_Scon.Open();

                if (Parameters != null)
                {
                    foreach (SqlParameter para in Parameters)
                    {
                        m_Scmd.Parameters.Add(para);
                    }
                }

                try
                {
                    m_Scmd.ExecuteNonQuery();

                }
                catch(Exception ex)
                //catch
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }

            

                //int rowsAffected = m_Scmd.ExecuteNonQuery();

                //if (rowsAffected > 0)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
            //}
            //else
            //{
            //    return false;
            //}
        }

        ///
        /// Used to execute stored procedure using executescalar method
        ///
        ///
        ///
        /// string
        public string ExecuteScalarSPDB(string ProcedureName, SqlParameter[] Parameters)
        {
            if (!string.IsNullOrEmpty(ProcedureName))
            {
                SqlConnection m_Scon = new SqlConnection(connStr);
                SqlCommand m_Scmd = new SqlCommand(ProcedureName, m_Scon);
                m_Scon.Close();
                m_Scon.Open();

                if (Parameters != null)
                {
                    foreach (SqlParameter para in Parameters)
                    {
                        m_Scmd.Parameters.Add(para);
                    }
                }

                object rowsID = m_Scmd.ExecuteScalar();
                return rowsID.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Executes reader to get a list of given type. The callback function fetches the data from data reader.
        /// </summary>
        /// <typeparam name="T">Expected Type return.</typeparam>
        /// <param name="storedProcedure">Stored procedure name.</param>
        /// <param name="parameters">Dictionary of parameter.</param>
        /// <param name="callback">Callback function that will fetch data from data reader.</param>
        /// <returns>List of expected type returned by the callback function.</returns>
        public static IEnumerable<T> ExecuteGetList<T>(string storedProcedure, IDictionary<string, object> parameters, Func<IDataReader, T> callback)
        {
            IList<SqlParameter> sqlParameterList = new List<SqlParameter>();
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    sqlParameterList.Add(new SqlParameter(parameter.Key, parameter.Value));
                }
            }

            return ExecuteGetList<T>(storedProcedure, callback, sqlParameterList.ToArray());
        }

        /// <summary>
        /// Executes reader to get a list of given type. The callback function fetches the data from data reader.
        /// </summary>
        /// <typeparam name="T">Expected Type return.</typeparam>
        /// <param name="storedProcedure">Stored procedure name.</param>
        /// <param name="callback">Callback function that will fetch data from data reader.</param>
        /// <param name="parameters">Array of parameter.</param>
        /// <returns>List of expected type returned by the callback function.</returns>
        public static IEnumerable<T> ExecuteGetList<T>(string storedProcedure, Func<IDataReader, T> callback, SqlParameter[] parameters)
        {
            using (SqlDataReader reader = new DBSqlHelper().ExecuteReaderSPDB(storedProcedure, parameters))
            {
                while (reader.Read())
                {
                    yield return callback(reader);
                }
            }
        }

        /// <summary>
        /// Executes reader to get the given type. The callback function fetches the data from data reader.
        /// </summary>
        /// <typeparam name="T">Expected Type return.</typeparam>
        /// <param name="storedProcedure">Stored procedure name.</param>
        /// <param name="parameters">Dictionary of parameter.</param>
        /// <param name="callback">Callback function that will fetch data from data reader.</param>
        /// <returns>Expected type returned by the callback function.</returns>
        public static T ExecuteGet<T>(string storedProcedure, IDictionary<string, object> parameters, Func<IDataReader, T> callback)
        {
            IList<SqlParameter> sqlParameterList = new List<SqlParameter>();
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    sqlParameterList.Add(new SqlParameter(parameter.Key, parameter.Value));
                }
            }

            return ExecuteGet<T>(storedProcedure, callback, sqlParameterList.ToArray());
        }

        /// <summary>
        /// Executes reader to get the given type. The callback function fetches the data from data reader.
        /// </summary>
        /// <typeparam name="T">Expected Type return.</typeparam>
        /// <param name="storedProcedure">Stored procedure name.</param>
        /// <param name="callback">Callback function that will fetch data from data reader.</param>
        /// <param name="parameters">Array of parameter.</param>
        /// <returns>Expected type returned by the callback function.</returns>
        public static T ExecuteGet<T>(string storedProcedure, Func<IDataReader, T> callback, SqlParameter[] parameters)
        {
            T returnValue = default(T);
            using (SqlDataReader reader = new DBSqlHelper().ExecuteReaderSPDB(storedProcedure, parameters))
            {
                while (reader.Read())
                {
                    returnValue = callback(reader);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Executes a non-query command for 'Create', 'Update' and 'Delete' actions.
        /// </summary>
        /// <param name="storedProcedure">Stored Procedure name.</param>
        /// <param name="parameters">Dictionary of parameter.</param>
        /// <returns>True if succeeded.</returns>
        public static bool ExecuteCUD(string storedProcedure, IDictionary<string, object> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                IList<SqlParameter> sqlParameterList = new List<SqlParameter>();
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    sqlParameterList.Add(new SqlParameter(parameter.Key, parameter.Value));
                }

                return ExecuteCUD(sqlParameterList.ToArray(), storedProcedure);
            }

            throw new ArgumentNullException();
        }

        /// <summary>
        /// Executes a non-query command for 'Create', 'Update' and 'Delete' actions.
        /// </summary>
        /// <param name="parameters">Array of parameter.</param>
        /// <param name="storedProcedure">Stored Procedure name.</param>
        /// <returns>True if succeeded.</returns>
        public static bool ExecuteCUD(SqlParameter[] parameters, string storedProcedure)
        {
            if (parameters.Length > 0)
            {
                return new DBSqlHelper().ExecuteNonQuerySPDB(storedProcedure, parameters);
            }

            throw new ArgumentNullException();
        }
    }   
}