using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using DAL;
using Model;

namespace Logic
{
    public class OrderService
    {
        private OrderDAO orderDAO = new OrderDAO();
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            //
            orders = orderDAO.GetAllOrders();
            // get all orderitems for all orders
            foreach (Order order in orders)
            {
                order.OrderItems = orderDAO.GetOrderItemsForOrderID(order.OrderID);

            }
            return orders;
            //try
            //{
            //    orders = orderDAO.GetAllOrders();
            //    // get all orderitems for all orders
            //    foreach(Order order in orders)
            //    {
            //        order.OrderItems = orderDAO.GetOrderItemsForOrderID(order.OrderID);

            //    }
            //    return orders;
            //}
            //catch (Exception)
            //{
            //    Order order = new Order()
            //    {
            //        OrderID = -1,
            //        PurchaseTime = (DateTime.Now),
            //        State = OrderState.Pending,
            //        Table = new Table(),
            //        Employee = new Employee(),

            //    };
            //    orders.Add(order);
            //    return orders;
            //}
        }
        
        public int SubmitOrder(Order order)
        {
            
            try
            {
                order.State = OrderState.Pending;
                order.PurchaseTime = DateTime.Now;

                int rows = orderDAO.AddNewOrder(order);
                int orderID = orderDAO.GetLastOrderID();
                order.OrderID = orderID;
                order.Payment = false;
                foreach (OrderItem oi in order.OrderItems)
                {
                    if (oi.Comment == null)
                    {
                        oi.Comment = "___";
                    }
                    oi.State = ItemState.Preparing;
                }

                orderDAO.AddOrderItem(order);
                return rows;
            }
            catch (Exception)
            {
               
                
                return -1;
            }


        }
        public List<Order> Get_Orders_For_BarKitchen(Role role)
        {

            List<Order> orders = orderDAO.GetOrders();
            try
            {
                WorkPlace workPlace;
                if (role == Role.Barman)
                {
                    workPlace = WorkPlace.Bar;
                }
                else
                {
                    workPlace = WorkPlace.Kitchen;
                }
                foreach (Order order in orders)
                {
                    List<OrderItem> orderitems = orderDAO.Get_OrderItems_For_BarKitchen(order.OrderID, workPlace);
                    order.OrderItems = orderitems;
                }
                
                return orders;
            }
            catch (Exception)
            {
                return new List<Order>();
            }



        }
        //------------------------------------------------------------------------------------------------------------------------------------
        public int PayOrder(Order order, PaymentMethod paymentMethod) //paying an order with a payment method
        {
            try
            {
                order.Payment = true;

                orderDAO.ChangePaymentState(order);

                return orderDAO.PayOrder(order, paymentMethod);
                
            }
            catch(Exception e )
            {

                return -1;
            }
        }
        public List<Order> GetOrdersToPay() // getting orders to pay
        {
            List<Order> orders = orderDAO.GetOrdersForPayment();
            try
            {
                foreach (Order order in orders)
                {
                    order.OrderItems = orderDAO.GetOrderItemsForOrderID(order.OrderID);
                }
                return orders;
            }
            catch
            {
                return new List<Order>();
            }
            

        }
        //------------------------------------------------------------------------------------------------------------------------------------
        public int ChangeOrderItemState(int orderID, OrderItem item)
        {
            try
            {
                return orderDAO.ChangeOrderItemState(orderID, item);
            }
            catch(Exception)
            {

                return -1;
            }
           
        }
        public Order GetOrderForTable(Table table)
        {
            Order order = orderDAO.GetOrderForTable(table);

            order.OrderItems = orderDAO.GetOrderItemsForOrderID(order.OrderID);

            return order;
            //try
            //{
                
            //}
            //catch (Exception)
            //{
            //    List<OrderItem> orderItems = new List<OrderItem>();
            //    Order order = new Order();
            //    order.OrderItems = orderItems;
            //    return order;
                
            //}
            

        }
        public void ChangeOrderStatus()
        {

        }
        
        
        public void GetOrderByStatus()
        {

        }
        public void GetOrdersForDate()
        {

        }
    }
}
