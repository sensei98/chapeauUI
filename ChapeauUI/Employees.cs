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
    public partial class Employees : ChapeauUI.BaseForm
    {
        private Logic.EmployeeService employee_Service = new Logic.EmployeeService();
        private List<Employee> employeesList; // make the list a global member so can be accessed from everywhere

        public Employees()
        {
            InitializeComponent();
            pnlAddNewEmployee.Hide();
            
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            FillEmployeeListView();
        }
        private void FillEmployeeListView()
        {
            employeesList = employee_Service.GetAllEmployees(); // fill the list
            lvwEmployees.Items.Clear();

            foreach (Employee emp in employeesList)
            {
                ListViewItem item = new ListViewItem(emp.EmployeeID.ToString());
                item.SubItems.Add(emp.FirstName);
                item.SubItems.Add(emp.LastName);
                item.SubItems.Add(emp.Role.ToString());
                item.Tag = emp;
                lvwEmployees.Items.Add(item);

            }
        }

        private void btnRemoveEmployee_Click(object sender, EventArgs e)
        {
            if (lvwEmployees.SelectedItems.Count < 1)
                return;

            Employee employee = lvwEmployees.SelectedItems[0].Tag as Employee;

            string message = $"Are you sure you want to remove {employee.FullName}??";
            string title = "Remove Employee";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult answer = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);

            if (answer == DialogResult.Yes)
            {
                employee_Service.RemoveEmployee(employee);
                MessageBox.Show($"{employee.FullName} was successfully removed!");
                FillEmployeeListView();
            }
            else
                MessageBox.Show("Error!");


        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            btnRemoveEmployee.Enabled = false;
            ShowPanel(new Employee(), "add");
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlAddNewEmployee.Hide();
            
            btnRemoveEmployee.Enabled = true;
            FillEmployeeListView();
        }

        private void btnAddNewEmployee_Click(object sender, EventArgs e)
        {
            if(btnAddEmployee.Text == "&Add New Employee")
            {
                Employee employee = new Employee
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Password = txtPassword.Text,
                    Pin = Convert.ToInt32(txtPin.Text),
                    Role = (Role)cmbRole.SelectedItem
                };

                string message = $"Are you sure you want to add {employee.FullName} as a {cmbRole.SelectedItem}?";
                string title = "Add New Employee";
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);

                if (result == DialogResult.Yes)
                {
                    employee_Service.AddNewEmployee(employee);
                    FillEmployeeListView();
                    MessageBox.Show($"{employee.FullName} is successfully Added as an Employee : {cmbRole.SelectedItem}!!");

                }
                else
                    return;
            }
            else
            {
                if (lvwEmployees.SelectedItems.Count <= 0)
                {
                    return;
                }
                Employee selected = lvwEmployees.SelectedItems[0].Tag as Employee;
                Employee employee = new Employee
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Password = txtPassword.Text,
                    Pin = Convert.ToInt32(txtPin.Text),
                    PhoneNumber = Convert.ToInt32(txtPhone.Text),
                    Role = (Role)cmbRole.SelectedItem
                };
                string message = $"are you sure you want to edit {employee.FullName} as a {cmbRole.SelectedItem}?";
                string title = "Edit Employee";
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                if (result == DialogResult.Yes)
                {
                    int rowsAffected = employee_Service.EditEmployee(employee, selected.EmployeeID);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"{employee.FullName} is successfully Edited");
                        FillEmployeeListView();
                    }
                    else
                    {
                        MessageBox.Show("an error occured, please contact developer.");
                    }
                }
                else
                {
                    return;
                }
                RefreshPanles();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lvwEmployees.SelectedItems.Count <= 0)
            {
                return;
            }
            Employee employee = lvwEmployees.SelectedItems[0].Tag as Employee;

            cmbRole.DataSource = (Enum.GetValues(typeof(Role)));

            pnlAddNewEmployee.Visible = true;
            FillEditDetails(employee);

        }
        private void FillEditDetails(Employee employee)
        {
            txtFirstName.Text = employee.FirstName;
            txtLastName.Text = employee.LastName;
            txtPassword.Text = employee.Password;
            txtPhone.Text = employee.PhoneNumber.ToString();
            txtPin.Text = employee.Pin.ToString();
            cmbRole.SelectedItem = employee.Role;
        }

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            if (lvwEmployees.SelectedItems.Count <= 0)
                return;

            Employee employee = lvwEmployees.SelectedItems[0].Tag as Employee;
            ShowPanel(employee, "edit");


        }
        private void ShowPanel(Employee employee, string task)
        {
            pnlAddNewEmployee.Visible = true;
            RefreshPanles();
            cmbRole.DataSource = (Enum.GetValues(typeof(Role)));
            if (task == "add")
            {
                btnAddEmployee.Text = "&Add New Employee";
            }
            else
            {
                btnAddEmployee.Text = "&Edit Employee";
                FillEditDetails(employee);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(lvwEmployees.SelectedItems.Count <= 0)
            {
                return;
            }
            Employee selected = lvwEmployees.SelectedItems[0].Tag as Employee;
            Employee employee = new Employee
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Password = txtPassword.Text,
                Pin = Convert.ToInt32(txtPin.Text),
                PhoneNumber = Convert.ToInt32(txtPhone.Text),
                Role = (Role)cmbRole.SelectedItem
            };
            string message = $"are you sure you want to edit {employee.FullName} as a {cmbRole.SelectedItem}?";
            string title = "Edit Employee";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
            if (result == DialogResult.Yes)
            {
                int rowsAffected = employee_Service.EditEmployee(employee, selected.EmployeeID);
                if (rowsAffected > 0)
                {
                    MessageBox.Show($"{employee.FullName} is successfully Edited");
                    FillEmployeeListView();
                }
                else
                {
                    MessageBox.Show("an error occured, please contact developer.");
                }
            }
            else
            {
                return;
            }
            RefreshPanles();



        }
        private void RefreshPanles()
        {
            txtPassword.Text = "";
            txtPhone.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPin.Text = "";


            
        }

        
    }
}
