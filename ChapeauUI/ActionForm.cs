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
    public partial class ActionForm : ChapeauUI.BaseForm
    {
        Employee user;
        Form previousForm;
        public ActionForm(Form previousForm, Employee user)
        {
            InitializeComponent();
            this.user = user;
            this.previousForm = previousForm;
        }

        

        private void btnReservations_Click(object sender, EventArgs e)
        {
            TableStates reservationForm = new TableStates(user);
            reservationForm.ShowDialog();

        }

        private void btnOrderStatus_Click(object sender, EventArgs e)
        {
            OrderView work = new OrderView(user);
            work.ShowDialog(); 
        }

        private void btnTakeOrders_Click(object sender, EventArgs e)
        {
            Payments payments = new Payments(user);
            payments.ShowDialog();
        }

        private void ActionForm_Load(object sender, EventArgs e)
        {
            if(user.Role != Role.Manager)
            {
                btnAddEmployee.Enabled = false;
            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Employees EmployeeForm = new Employees();
            EmployeeForm.ShowDialog();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            StockForm stockForm = new StockForm(user);
            stockForm.ShowDialog();

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            previousForm.Show();
            this.Close();
        }

        private void ActionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.previousForm.Show();
        }
    }
}
