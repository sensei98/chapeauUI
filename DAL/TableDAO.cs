using Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class TableDAO : Base
    {
        public List<Table> GetAllTables()
        {
            List<Table> tables = new List<Table>();
            SqlCommand cmd = new SqlCommand("SELECT tableID, capacity, [state] FROM [Tables];", dbConnection);
            dbConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Table table = ReadTable(reader);
                tables.Add(table);
            }
            reader.Close();
            dbConnection.Close();
            return tables;

        }
        public int ChangeTableStatus(Table table, TableState state)
        {
            SqlCommand cmd = new SqlCommand("UPDATE TABLES SET [state] = @state WHERE tableID = @id", dbConnection);
            cmd.Parameters.AddWithValue("@state", state);
            cmd.Parameters.AddWithValue("@id", table.TableID);

            dbConnection.Open();
            int rows = cmd.ExecuteNonQuery();
            dbConnection.Close();

            return rows;

        }
        private Table ReadTable(SqlDataReader reader)
        {
            Table table = new Table
            {
                TableID = (int)reader["tableID"],
                Capacity = (int)reader["capacity"],
                State = (TableState)reader["state"]
            };
            
            return table;
        }
    }
}
