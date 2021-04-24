using Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Globalization;

namespace DAL
{
    public class OrderDAO : Base
    {
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            SqlCommand cmd = new SqlCommand("SELECT O.orderID, O.purchaseTime, O.tableID, O.orderState, " +
                "C.comment, O.totalPrice, C.productID, C.EmployeeID, C.quantity, E.employeeID, E.firstName, " +
                "E.lastName, E.roleID, T.capacity, T.[state] FROM Orders AS O JOIN OrderItems AS C ON C.OrderID = O.OrderID " +
                "JOIN Employees AS E ON C.EmployeeID = E.employeeID JOIN [Tables] AS T ON T.tableID = O.tableID", dbConnection); // joins with emp and table
            dbConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Order order = ReadOrder(reader);
                orders.Add(order);
            }
            reader.Close();
            dbConnection.Close();
            return orders;
        }
        
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            SqlCommand cmd = new SqlCommand("SELECT O.orderID, O.purchaseTime, O.orderState, T.tableID, T.[state] FROM Orders AS O JOIN [TABLES] AS T ON O.tableID = T.tableID", dbConnection); // joins with emp and table
            dbConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Order order = new Order
                {
                    OrderID = (int)reader["orderID"],
                    PurchaseTime = (DateTime)reader["purchaseTime"],
                    State = (OrderState)reader["orderState"]
                };
                Table t = new Table
                {
                    TableID = (int)reader["tableID"],
                    State = (TableState)reader["state"]
                };
                order.Table = t;
                
                orders.Add(order);
            }
            reader.Close();
            dbConnection.Close();
            return orders;


        }
        public List<OrderItem> Get_OrderItems_For_BarKitchen(int orderID, WorkPlace workplace)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            SqlCommand sqlCommand = new SqlCommand("SELECT C.productID, C.quantity, C.StateID, C.comment,  C.submissionTime, S.menuItem, S.price FROM OrderItems AS C JOIN Stock AS S ON S.productID = C.productID WHERE C.orderID = @id AND S.workPlace = @work", dbConnection);
            sqlCommand.Parameters.AddWithValue("@id", orderID);
            sqlCommand.Parameters.AddWithValue("@work", workplace);
            dbConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {

                OrderItem orderItem = read_OrderItem(reader);
                orderItems.Add(orderItem);
            }
            reader.Close();
            dbConnection.Close();


            return orderItems;
        }

        public List<OrderItem> GetOrderItemsForOrderID(int orderID)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            SqlCommand cmd = new SqlCommand("SELECT C.productID, C.quantity, C.submissionTime, C.StateID, C.comment, S.menuItem, S.price FROM OrderItems AS C JOIN Stock AS S ON S.productID = C.productID WHERE C.orderID = @id", dbConnection);
            cmd.Parameters.AddWithValue("@id", orderID);
            
            dbConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                OrderItem orderitm = read_OrderItem(reader);
                orderItems.Add(orderitm);
            }
            reader.Close();
            dbConnection.Close();


            return orderItems;
        }

        private OrderItem read_OrderItem(SqlDataReader reader)
        {
            MenuItem menu = new MenuItem
            {
                ProductID = (int)reader["productID"],
                Name = (string)reader["menuItem"],
                Price = (decimal)reader["price"]
                
            };
            int qty = (int)reader["quantity"];
            string comment = (string)reader["comment"];

            ItemState state = (ItemState)reader["stateID"];
            OrderItem item = new OrderItem { item = menu, Quantity = qty, State = state, Comment = comment };

            return item;
        }

        public int AddNewOrder(Order order)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Orders(purchaseTime, totalPrice, orderState, tableID, paymentStatus) VALUES (@time, @total, @state, @table, @pay);", dbConnection);
            cmd.Parameters.AddWithValue("@time", order.PurchaseTime);
            cmd.Parameters.AddWithValue("@total", order.TotalPrice);
            cmd.Parameters.AddWithValue("@state", order.State);
            cmd.Parameters.AddWithValue("@table", order.Table.TableID);
            cmd.Parameters.AddWithValue("@pay", order.Payment);

            dbConnection.Open();
            int rows = cmd.ExecuteNonQuery();
            dbConnection.Close();
            return rows;

        }
        
        public int AddOrderItem(Order order) //adding an order to a orderItems  // not complete //add AddOrderitem()
        {
            SqlCommand cmd;
            dbConnection.Open();
            int row = 0;
            foreach (OrderItem mi in order.OrderItems)
            {
                string query = $"INSERT INTO OrderItems(OrderID, productID, quantity, totalPrice, employeeID, comment, workPlace, submissionTime, StateID) " +
                    $"VALUES(@orderid, @product, @quantity, @totalPrice, @employeeID, @comment, @workplace, @time, @state);";
                cmd = new SqlCommand(query, dbConnection);
                cmd.Parameters.AddWithValue("@orderid", order.OrderID);
                cmd.Parameters.AddWithValue("@product", mi.item.ProductID);
                cmd.Parameters.AddWithValue("@quantity", mi.Quantity);
                cmd.Parameters.AddWithValue("@totalPrice", mi.TotalPrice);
                cmd.Parameters.AddWithValue("@employeeID", order.Employee.EmployeeID);
                cmd.Parameters.AddWithValue("@comment", mi.Comment);
                cmd.Parameters.AddWithValue("@workplace", mi.item.WorkPlace);
                cmd.Parameters.AddWithValue("@time", DateTime.Now);
                cmd.Parameters.AddWithValue("@state", mi.State);
                row += cmd.ExecuteNonQuery();
            }

            dbConnection.Close();
            return row;
        }
        public int GetLastOrderID()
        {
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 orderID FROM orders ORDER BY orderID DESC", dbConnection);
            dbConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int orderID = (int)reader["orderID"];
            dbConnection.Close();
            return orderID;

        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public List<Order> GetOrdersForPayment() //
        {
            List<Order> unpaidList = new List<Order>();

            SqlCommand sqlCommand = new SqlCommand("SELECT O.orderID, O.purchaseTime, O.tableID, O.orderState , O.totalPrice, O.paymentStatus, T.capacity, T.[state] " +
                "FROM Orders AS O JOIN [Tables] AS T ON T.tableID = O.tableID WHERE O.paymentStatus = 0", dbConnection);
            dbConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Table table = new Table
                {
                    TableID = (int)reader["tableID"]
                };
                Order order = new Order
                {
                    OrderID = (int)reader["orderID"],
                    PurchaseTime = (DateTime)reader["purchaseTime"],
                    Total = (decimal)reader["totalPrice"],
                    Payment = (bool)reader["paymentStatus"]

                };
                order.Table = table;
                unpaidList.Add(order);


            }
            reader.Close();
            dbConnection.Close();
            return unpaidList;

        }
        public int PayOrder(Order order, PaymentMethod paymentMethod) //roundng up order to make payment with a method
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Payments(orderID, methodID, billAmount, vatAmount, tip, employeeID) " +
                "VALUES(@id, @method, @bill, @vat, @tip, @empID)", dbConnection);
            sqlCommand.Parameters.AddWithValue("@id", order.OrderID);
            sqlCommand.Parameters.AddWithValue("@method", paymentMethod);
            sqlCommand.Parameters.AddWithValue("@bill", order.Total);
            sqlCommand.Parameters.AddWithValue("@vat", order.TotalVat);
            sqlCommand.Parameters.AddWithValue("@tip", order.Tip);
            sqlCommand.Parameters.AddWithValue("empid", order.Employee.EmployeeID);

            dbConnection.Open();
            int payOrders = sqlCommand.ExecuteNonQuery();
            dbConnection.Close();

            return payOrders;
        
        }
        public int ChangePaymentState(Order order) //updating the state
        {
            SqlCommand sqlCommand = new SqlCommand("UPDATE Orders SET paymentStatus = @STATE, VAT = @vat, tip = @tip WHERE orderID = @ID", dbConnection);
            sqlCommand.Parameters.AddWithValue("@STATE", order.Payment);
            sqlCommand.Parameters.AddWithValue("@ID", order.OrderID);
            sqlCommand.Parameters.AddWithValue("@vat", order.TotalVat);
            sqlCommand.Parameters.AddWithValue("@tip", order.Tip);

            dbConnection.Open();
            int paymentState = sqlCommand.ExecuteNonQuery();
            dbConnection.Close();

            return paymentState;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private OrderItem ReadOrderItem(SqlDataReader reader)
        {
            MenuItem item = new MenuItem // reading the menu item from the table
            {
                ProductID = (int)reader["productID"],
                Name = (string)reader["menuItem"],
                Price = (decimal)reader["price"],
                Category = (Category)reader["category"],
                
                PreparationTime = (int)reader["preparationTime"]

            };
            int quantity = (int)reader["itemQty"]; // reading qty for it
            OrderItem orderItem = new OrderItem { item = item, Quantity = quantity }; // adding it to the list

            return orderItem;
        }
        private Order ReadOrder(SqlDataReader reader) //includes the order,employee and table
        {
            Order order = new Order
            {
                OrderID = (int)reader["OrderID"],
                State = (OrderState)reader["orderState"],
                PurchaseTime = (DateTime)reader["purchaseTime"]
            };

            Employee server = new Employee
            {
                FirstName = (string)reader["firstName"],
                LastName = (string)reader["lastName"],
                EmployeeID = (int)reader["employeeID"]
            };

            Table table = new Table
            {
                TableID = (int)reader["tableID"],
                Capacity = (int)reader["capacity"],
                State = (TableState)reader["state"]
            };

            order.Table = table;
            order.Employee = server;

            return order;
        }
        public int ChangeOrderItemState(int orderID, OrderItem item)
        {
            SqlCommand cmd = new SqlCommand("UPDATE OrderItems SET stateID = @stid WHERE orderID = @oid AND productID = @pid", dbConnection);
            cmd.Parameters.AddWithValue("@stid", item.State);
            cmd.Parameters.AddWithValue("@oid", orderID);
            cmd.Parameters.AddWithValue("@pid", item.item.ProductID);
            dbConnection.Open();

            int rows = cmd.ExecuteNonQuery();
            dbConnection.Close();



            return rows;
        }
        public void GetOrdersForDate()
        {

        }
        public void GetOrdersFor()
        {

        }
        public void ChangeOrderStatus()
        {

        }
        public Order GetOrderForTable(Table table)
        {
            SqlCommand cmd = new SqlCommand("SELECT O.orderID, O.purchaseTime, O.vat, O.tip, O.tableID, " +
                "O.paymentStatus, O.orderState, O.totalPrice, T.capacity, T.[state] FROM Orders AS O " +
                "JOIN [Tables] AS T ON T.tableID = O.tableID WHERE O.tableID = 1 AND O.paymentStatus = 0", dbConnection);
            cmd.Parameters.AddWithValue("@id", table.TableID);
            //cmd.Parameters.AddWithValue("@stat", false);


            dbConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Order order = new Order
            {
                OrderID = (int)reader["orderID"],
                Payment = (bool)reader["paymentStatus"],
                State = (OrderState)reader["orderState"],
                Total = (decimal)reader["totalPrice"]
                //Tip = (decimal)reader["tip"],
                //TotalVat = (decimal)reader["vat"]
            };
            reader.Close();
            dbConnection.Close();

            return order;

        }

    }
}
