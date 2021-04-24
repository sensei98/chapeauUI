using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;
using Logic;

namespace ChapeauUI
{
    public partial class OrderView : ChapeauUI.BaseForm
    {
        Employee employee;
        OrderService orderService;
        List<Order> orders;
        Role role;
        public OrderView(Employee user)
        {
            InitializeComponent();
            this.employee = user;
            orderService = new OrderService();
            orders = new List<Order>();
            this.role = employee.Role;
        }

        private void WorkPlace_Load(object sender, EventArgs e)
        {
            lbl_Message.Visible = false;
            if (employee.Role == Role.Waiter)
            {
                btn_Ready.Enabled = false;
                lbl_Message.Visible = true;
                lbl_Message.Text = "You cannot change the state";
                
            }
            if(employee.Role == Role.Barman || employee.Role == Role.KitchenStaff)
            {
                btn_Change.Enabled = false;
                
            }

            Fill_Orders(orders, this.role);

        }

        private void Fill_Orders(List<Order> orders, Role role)
        {
            
            orders = orderService.Get_Orders_For_BarKitchen(role);
            listview_Orders.Items.Clear();
            

            foreach (Order order in orders)
            {
                foreach(OrderItem orderItem in order.OrderItems)
                {
                    ListViewItem item = new ListViewItem(order.OrderID.ToString());
                    item.SubItems.Add(orderItem.item.Name);
                    item.SubItems.Add(orderItem.Comment);
                    item.SubItems.Add(orderItem.Quantity.ToString());
                    item.SubItems.Add(order.PurchaseTime.ToString("hh:mm:ss"));
                    item.SubItems.Add(order.Table.TableID.ToString());
                    item.SubItems.Add(orderItem.State.ToString());
                    item.Tag = orderItem;
                    listview_Orders.Items.Add(item);
                }
            }

            
        }


        private void btn_Ready_Click(object sender, EventArgs e)
        {
            if (listview_Orders.SelectedItems.Count <= 0)
            {
                return;
            }

            OrderItem item = listview_Orders.SelectedItems[0].Tag as OrderItem;
            item.State = ItemState.Ready;

            string subtitle = $"Do you want to change state from preparing to ready?";
            string title = $"Ready to serve";

            MessageBoxButtons msgButton = MessageBoxButtons.YesNo;
            DialogResult dialogResult = MessageBox.Show(subtitle, title, msgButton, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.Yes)
            {
                int orderid = int.Parse(listview_Orders.SelectedItems[0].Text);
                int rows = orderService.ChangeOrderItemState(orderid, item);

                if (rows > 0)
                {
                    MessageBox.Show("Order is marked: Ready");
                }
                else
                {
                    MessageBox.Show("A problem has occurred");
                }

                WorkPlace_Load(sender, e);
            }
            else
            {
                return;
            }
        }

        private void btn_Change_Click(object sender, EventArgs e)
        {
            if(role == Role.Barman)
            {
                role = Role.KitchenStaff;
            }
            else
            {
                role = Role.Barman;
            }
            Fill_Orders(orders, role);

        }
    }
}
