using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Logic;
using Model;
using MenuItem = Model.MenuItem; // to prevent ambig ref err

namespace ChapeauUI
{
    public partial class TakeOrder : ChapeauUI.BaseForm
    {
        MenuItemService menuSrv;
        private string submenu;
        Employee user;
        Order order;
        OrderService orderSrv;
        TableService tbSrv;
        Table table;
        Form previousForm;
        
        public TakeOrder(Form previousForm, string submenu, Employee user, Table table)
        {
            InitializeComponent();
            menuSrv = new MenuItemService();
            orderSrv = new OrderService();
            tbSrv = new TableService();
            this.previousForm = previousForm;
            this.table = table;
            this.submenu = submenu;
            this.user = user;
            
        }
        private void StartOrder()
        {
            this.order = new Order();
            this.order.OrderItems = new List<OrderItem>();
            
            order.Table = this.table;
            order.Employee = user;
            numUpdQuantity.Value = 0;
            chbComment.Checked = false;
            txtComment.Text = "";
            lstMenu.SelectedItems.Clear();
            lblTotalPrice.Text = "___";
            FillSelectedItems();

        }

        private void TakeOrder_Load(object sender, EventArgs e)
        {
            StartOrder();
            FillMenu();
            txtComment.Enabled = false;
            
        }
        private void FillMenu()
        {
            List<MenuItem> items = menuSrv.GetSubMenu(submenu);
            lstMenu.Items.Clear();
            foreach(MenuItem item in items)
            {
                ListViewItem i = new ListViewItem(item.Name);
                i.SubItems.Add(item.Price.ToString("0.00"));
                i.Tag = item;
                lstMenu.Items.Add(i);
            }


        }

        

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (lstMenu.SelectedItems.Count < 1 || numUpdQuantity.Value <= 0)
            {
                MessageBox.Show("please select an item and a quantity");
                return;
            }
            

            MenuItem item = lstMenu.SelectedItems[0].Tag as MenuItem;
            int quantity = Convert.ToInt32(numUpdQuantity.Value);
            
            OrderItem orderItem = new OrderItem { item = item, Quantity = quantity };

            if(chbComment.Enabled && txtComment.Text != null)
            {
                orderItem.Comment = txtComment.Text;
            }
            else
            {
                orderItem.Comment = "___";
            }

            bool contains = false;
            foreach(OrderItem oitm in order.OrderItems)
            {
                if(oitm.item.Name == orderItem.item.Name)
                {
                    
                    contains = true;
                    break;
                }

            }
            

            if (contains)
            {
                foreach (OrderItem oitm in order.OrderItems)
                {
                    if (oitm.item.Name == item.Name)
                    {
                        
                        orderItem.Quantity += oitm.Quantity;
                        order.OrderItems.Remove(oitm);
                        break;
                    }
                }
                
            }
            order.OrderItems.Add(orderItem);
            FillSelectedItems();


            decimal total = 0;
            foreach (OrderItem o in order.OrderItems)
            {
                total += o.TotalPrice;
            }
            lblTotalPrice.Text = total.ToString("0.00");

        }
        private void FillSelectedItems()
        {
            lstSelected.Items.Clear();
            foreach (OrderItem item in order.OrderItems)
            {
                ListViewItem i = new ListViewItem(item.item.Name);
                i.SubItems.Add(item.Quantity.ToString());
                i.SubItems.Add(item.Comment);
                i.Tag = item;
                lstSelected.Items.Add(i);

            }
        }

        private void btnChangeMenu_Click(object sender, EventArgs e)
        {
            MenuCardSelection selection = new MenuCardSelection(DateTime.Now, user);
            
            // show different menus
            DialogResult sel = selection.ShowDialog();
            
            this.submenu = selection.ReturnValue;
            FillMenu();
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            string message = $"do you want to remove the whole order?";
            string title = "Remove Order";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
            if (result == DialogResult.Yes)
            {
                StartOrder();
            }
            else
            {
                return;
            }
            FillSelectedItems();
        }

        private void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            if(order.OrderItems.Count <= 0)
            {
                MessageBox.Show("please select Items to submit an order");
                return;
            }

            int rows = orderSrv.SubmitOrder(order);

            if(rows > 0)
            {
                MessageBox.Show("order was successfully sent");

            }

            tbSrv.ChangeTableStatus(order.Table, TableState.OrderTaken);

            StartOrder();
            
        }

        private void lstMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            numUpdQuantity.Value = 0;
            chbComment.Checked = false;
            txtComment.Text = "";

        }

        private void lstSelected_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(lstSelected.SelectedItems.Count <= 0)
            {
                return;
            }

            OrderItem item = lstSelected.SelectedItems[0].Tag as OrderItem;

            string message = $"do you want to remove {item.item.Name}?";
            string title = "Remove item";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
            if (result == DialogResult.Yes)
            {
                order.OrderItems.Remove(item);
            }
            else
            {
                return;
            }
            FillSelectedItems();

        }

        private void chbComment_CheckedChanged(object sender, EventArgs e)
        {
            if (txtComment.Enabled)
            {
                txtComment.Enabled = false;
            }
            else
            {
                txtComment.Enabled = true;
            }
        }

        

        
    }
}
