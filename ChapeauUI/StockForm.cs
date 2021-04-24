using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;
using Logic;
using MenuItem = Model.MenuItem;

namespace ChapeauUI
{
    public partial class StockForm : ChapeauUI.BaseForm
    {
        MenuItemService menuItemSrv;
        Employee user;
        public StockForm(Employee user)
        {
            InitializeComponent();
            menuItemSrv = new MenuItemService();
            this.user = user;
        }

        private void StockForm_Load(object sender, EventArgs e)
        {
            FillListView();
            if(user.Role != Role.Manager)
            {
                pnlAddNew.Enabled = false;
                pnlButtons.Enabled = false;
                pnlQuantity.Enabled = false;
            }
        
        }
        private void FillListView()
        {
            List<MenuItem> menuItems = menuItemSrv.GetAllMenuItems();

            lstMenu.Items.Clear();
            foreach (MenuItem mi in menuItems)
            {
                ListViewItem lvItem = new ListViewItem(mi.ProductID.ToString());
                lvItem.SubItems.Add(mi.Name);
                lvItem.SubItems.Add(mi.Quantity.ToString());
                lvItem.SubItems.Add(mi.Price.ToString());
                lvItem.SubItems.Add(mi.PreparationTime.ToString());
                lvItem.SubItems.Add(mi.Category.ToString());
                lvItem.Tag = mi;
                lstMenu.Items.Add(lvItem);

            }
        }

        

        private void btnSubmitQuantity_Click(object sender, EventArgs e)
        {
            if(lstMenu.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please select an item");
                return;
            }
            MenuItem selectedItem = lstMenu.SelectedItems[0].Tag as MenuItem;

            selectedItem.Quantity = Convert.ToInt32(numQchange.Value);

            menuItemSrv.AddStockAmount(selectedItem);
            numQchange.Value = 0;
            FillListView();
        }

        private void lstMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lstMenu.SelectedItems.Count <= 0)
            {
                return;
            }
            MenuItem selectedItem = lstMenu.SelectedItems[0].Tag as MenuItem;
            
            numQchange.Value = selectedItem.Quantity;




        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            pnlAddNew.Enabled = true;
            if(lstMenu.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please select an item");
                return;

            }

            MenuItem selectedItem = lstMenu.SelectedItems[0].Tag as MenuItem;

            txtName.Text = selectedItem.Name;
            txtPrice.Text = selectedItem.Price.ToString();
            cmbCategory.DataSource = Enum.GetValues(typeof(Category));
            cmbCategory.SelectedItem = selectedItem.Category;
            numPrep.Value = selectedItem.PreparationTime;
            numQuantity.Value = selectedItem.Quantity;
            lblProductID.Text = selectedItem.ProductID.ToString();

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(txtName.Text == null || txtPrice.Text == null || cmbCategory.SelectedIndex <= 0)
            {
                MessageBox.Show("please fill all the required fields");
            }
            else
            {
                MenuItem currentItem;
                bool ExistingItem = int.TryParse(lblProductID.Text, out int pID);
                if (ExistingItem)
                {

                    //
                    currentItem = new MenuItem
                    {
                        Name = txtName.Text,
                        ProductID = pID,
                        PreparationTime = Convert.ToInt32(numPrep.Value),
                        Category = (Category)cmbCategory.SelectedItem,
                        Price = Convert.ToDecimal(txtPrice.Text),
                        Quantity = Convert.ToInt32(numQuantity.Value),
                    };
                    int rows = menuItemSrv.EditMenuItem(currentItem);
                    ShowResult(rows, "item edited sucessfully", "btn submit, add menu item");
                }
                else
                {
                    currentItem = new MenuItem
                    {
                        Name = txtName.Text,
                        //ProductID = Convert.ToInt32(lblProductID.Text),
                        PreparationTime = Convert.ToInt32(numPrep.Value),
                        Category = (Category)cmbCategory.SelectedItem,
                        Price = Convert.ToDecimal(txtPrice.Text),
                        Quantity = Convert.ToInt32(numQuantity.Value),
                    };
                    int rows = menuItemSrv.AddMenuItem(currentItem);

                    ShowResult(rows, "item added sucessfully", "btn submit click, edit menu item");
                }
            }
            


            

            RefreshPanel();
            FillListView();

        }
        private void RefreshPanel()
        {
            txtName.Text = "";
            txtPrice.Text = "";
            lblProductID.Text = "___";
            numPrep.Value = 0;
            numQuantity.Value = 0;



        }

        private void btnAddNewItem_Click(object sender, EventArgs e)
        {
            RefreshPanel();
            pnlAddNew.Enabled = true;
        
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if(lstMenu.SelectedItems.Count <= 0)
            {
                return;
            }

            MenuItem currentItem = lstMenu.SelectedItems[0].Tag as MenuItem;

            int rows = menuItemSrv.RemoveMenuItem(currentItem);
            ShowResult(rows,"item successfully deleted", "btn delete item");
            FillListView();

        }
        private void ShowResult(int rows, string action, string place)
        {
            if (rows > 0)
            {
                if (rows > 0)
                {
                    MessageBox.Show(action);
                }
                else
                {
                    MessageBox.Show("an unexpected problem occured.");
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void numQchange_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
