using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Logic;
using Model;

namespace ChapeauUI
{
    public partial class Payments : ChapeauUI.BaseForm
    {
        Logic.OrderService order_Service;
        Employee employee;
        Order orderToPay;

        public Payments(Employee employee)
        {
            InitializeComponent();
            order_Service = new Logic.OrderService();
            this.employee = employee;

        }

        private void Payments_Load(object sender, EventArgs e)
        {
            LoadPayments();
            txtTip.Enabled = false; //settting it to false when opened at first

        }
        private void LoadPayments()
        {
            List<Order> orders = order_Service.GetOrdersToPay(); //get the orders for payment
            foreach(Order order in orders) //accessing the tablenumber for each order
            {
                cmbTables.Items.Add(order.Table.TableID);
                cmbTables.Tag = order; //stores the object reference (links object to control)
            }
        }

        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.orderToPay = cmbTables.Tag as Order; // binds the order to pay to the table as an order....

            this.orderToPay.Employee = employee; //.....

            lblTableNo.Text = orderToPay.Table.TableID.ToString(); //getting the table number
            lblBill.Text = orderToPay.Total.ToString(); //getting the bill

           

            foreach(OrderItem orderItem in orderToPay.OrderItems)//filling the listview with ordered items
            {
                ListViewItem item = new ListViewItem(orderToPay.OrderID.ToString());
                item.SubItems.Add(orderItem.item.Name);
                item.SubItems.Add(orderItem.Quantity.ToString());
                item.SubItems.Add(orderItem.TotalPrice.ToString());
                item.Tag = orderItem; //binds the orderitem object to the listview item
                lstItems.Items.Add(item);
            }
            cmbPaymentMethod.DataSource = (Enum.GetValues(typeof(PaymentMethod))); // loads the payment methods when a table is clicked

            decimal VAT = 0;
            foreach (OrderItem item in orderToPay.OrderItems) //vat for alcoholic and non alcoholic drinks
            {
                if (item.item.Category == Category.Beers || item.item.Category == Category.Wines)
                {
                    VAT += item.item.Price * item.Quantity * 0.21M; //why the M (decimal to double)
                }
                else
                {
                    VAT += item.item.Price * item.Quantity * 0.09M;
                }
            }
            orderToPay.TotalVat = VAT; //store that here

            lblVAT.Text = VAT.ToString("0.00");
            lblTotalVatBill.Text = (orderToPay.Total + VAT).ToString("0.00");

        }

        

        private void chbTip_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTip.Checked)
                txtTip.Enabled = true;

            else
                txtTip.Enabled = false;
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstItems.SelectedItems.Count <= 0)
                return;
            
            OrderItem orderItem = lstItems.SelectedItems[0].Tag as OrderItem;// makes it possible to get the object back from listviewitem from anywhere in the code(thats why you cast the object back as tag)

            lblComments.Text = orderItem.item.Name; // MAKES IT POSSIBLE TO GET THE NAME OF THE ORDER ON THE COMMENTS TEXT
        }

        private void btnSubmitCm_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedItems.Count <= 0 || txtComments.Text == null)
                return; //terminates it (ends)
            
            OrderItem orderItem = lstItems.SelectedItems[0].Tag as OrderItem;//

            string comment;
            if(txtComments.Text != null)
            {
                comment = txtComments.Text;
            }
            else
            {
                comment = "No comments added";
            }

            orderItem.Comment = comment;

            MessageBox.Show("Comment is added");
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            
            decimal tip = 0;
            bool validTip = decimal.TryParse(txtTip.Text, out tip);//converting tip to decimal
            if (chbTip.Checked && !validTip)
            {
                MessageBox.Show("Please enter a valid tip amount!");
            }

            PaymentMethod paymentMethod = (PaymentMethod)cmbPaymentMethod.SelectedItem; //casting the payment methods to the combo box.selected items

            orderToPay.Tip = tip;
            int paid = order_Service.PayOrder(orderToPay, paymentMethod);
            if(paid > 0)
            {
                MessageBox.Show("order is successfully paid");
            }
            else
            {
                MessageBox.Show("an error occured");
            }
        }
    }
}
