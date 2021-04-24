using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public abstract class Base
    {
        protected SqlConnection dbConnection;
        private SqlDataAdapter adapter;
        private SqlConnection conn;
        public Base()
        {
            string connString = ConfigurationManager.ConnectionStrings["CHAPEAU DATABASE"].ConnectionString;
            dbConnection = new SqlConnection(connString);
            adapter = new SqlDataAdapter();
            conn = new SqlConnection();

        }
        private void CloseConnection()
        {
            conn.Close();
        }
        protected SqlConnection OpenConnection()
        {
            if (dbConnection.State == ConnectionState.Closed || dbConnection.State == ConnectionState.Broken)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }
        protected void ExecuteEditQuery(String query, SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();

            try
            {
                command.Connection = OpenConnection();
                command.CommandText = query;
                command.Parameters.AddRange(sqlParameters);
                adapter.InsertCommand = command;
                command.ExecuteNonQuery();
            }
            catch (SqlException )
            {
                // Print.ErrorLog(e);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        /* For Select Queries */
        protected DataTable ExecuteSelectQuery(String query, params SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();
            DataTable dataTable;
            DataSet dataSet = new DataSet();

            try
            {
                command.Connection = OpenConnection();
                command.CommandText = query;
                command.Parameters.AddRange(sqlParameters);
                command.ExecuteNonQuery();
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
            }
            catch (SqlException)
            {
                // Print.ErrorLog(e);
                return null;
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }
    }
}
