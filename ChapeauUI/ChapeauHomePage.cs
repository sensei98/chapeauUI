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
    public partial class ChapeauHomePage : ChapeauUI.BaseForm
    {
        EmployeeService empSrv;
        Employee user;
        public ChapeauHomePage()
        {
            InitializeComponent();
            empSrv = new EmployeeService();

            txtPassword.UseSystemPasswordChar = true;
            txtPassword.PasswordChar = '*';

        }

        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            bool validUsername = int.TryParse(txtUsername.Text, out int username); 
            
            string password = txtPassword.Text;

            
            if (validUsername)
            {
                user = empSrv.GetEmployeeInfo(username, password);
            }
            if (user == null)
            {
                MessageBox.Show("username or password is incorrect");
            }
            else
            {
                this.Hide();
                ActionForm act = new ActionForm(this, user);
                act.ShowDialog();
                
                
            }
            txtPassword.Text = "";
            txtUsername.Text = "";
            user = null;
        }
    }
}
