using Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class MenuItemDAO : Base
    {
        public List<MenuItem> GetAllMenuItems()
        {
            List<MenuItem> items = new List<MenuItem>();
            SqlCommand cmd = new SqlCommand("SELECT productID, menuItem, quantity, price, preparationTime, category FROM Stock", dbConnection);
            dbConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MenuItem item = ReadMenuItem(reader);
                items.Add(item);
            }
            reader.Close();
            dbConnection.Close();
            return items;
        }
        private MenuItem ReadMenuItem(SqlDataReader reader)
        {
            MenuItem item = new MenuItem
            {
                ProductID = (int)reader["productID"],
                Name = (string)reader["menuItem"],
                Price = (decimal)reader["price"],
                Category = (Category)reader["category"],
                PreparationTime = (int)reader["preparationTime"],
                Quantity = (int)reader["quantity"]
            };
            
            return item;
        }
        public List<MenuItem> GetSubMenu(Category category)
        {
            List<MenuItem> items = new List<MenuItem>();
            SqlCommand cmd = new SqlCommand("SELECT productID, menuItem, quantity, price, preparationTime, category FROM stock WHERE category = @cat", dbConnection);
            cmd.Parameters.AddWithValue("@cat", category);
            dbConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                MenuItem item = ReadMenuItem(reader);
                items.Add(item);
            }
            reader.Close();
            dbConnection.Close();
            return items;

        }
        public int EditMenuItem(MenuItem item)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Stock SET quantity = @qty, menuItem = @name, price = @price, " +
                "preparationTime = @prep, category = @cat WHERE productID = @id", dbConnection);
            cmd.Parameters.AddWithValue("@qty", item.Quantity);
            cmd.Parameters.AddWithValue("@name", item.Name);
            cmd.Parameters.AddWithValue("@price", item.Price);
            cmd.Parameters.AddWithValue("@prep", item.PreparationTime);
            cmd.Parameters.AddWithValue("@cat", item.Category);
            cmd.Parameters.AddWithValue("@id", item.ProductID);

            dbConnection.Open();

            int rows = cmd.ExecuteNonQuery();

            dbConnection.Close();

            return rows;
        }
        public int AddStockAmount(MenuItem item)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Stock SET quantity = @qty WHERE productID = @id", dbConnection);
            cmd.Parameters.AddWithValue("@qty", item.Quantity);
            cmd.Parameters.AddWithValue("@id", item.ProductID);

            dbConnection.Open();
            int rows = cmd.ExecuteNonQuery();
            dbConnection.Close();

            return rows;

        }
        public MenuItem GetItemForID(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT productID, menuItem, quantity, price, preparationTime, category FROM Stock WHERE productID = @id", dbConnection);
            cmd.Parameters.AddWithValue("@id", id);

            dbConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            MenuItem item = ReadMenuItem(reader);

            dbConnection.Close();
            return item;

        }
        public int RemoveMenuItem(MenuItem item)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Stock WHERE productID = @id", dbConnection);
            cmd.Parameters.AddWithValue("@id", item.ProductID);

            dbConnection.Open();
            int rows = cmd.ExecuteNonQuery();
            dbConnection.Close();

            return rows;

        }
        public int AddMenuItem(MenuItem item)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Stock(menuItem, quantity, price, preparationTime, category) " +
                "VALUES(@item, @qty, @price, @prep, @cat);", dbConnection);

            cmd.Parameters.AddWithValue("@item", item.Name);
            cmd.Parameters.AddWithValue("@qty", item.Quantity);
            cmd.Parameters.AddWithValue("@price", item.Price);
            cmd.Parameters.AddWithValue("@prep", item.PreparationTime);
            cmd.Parameters.AddWithValue("@cat", item.Category);
            dbConnection.Open();

            int rows = cmd.ExecuteNonQuery();

            dbConnection.Close();
            return rows;
        }
    }
}
