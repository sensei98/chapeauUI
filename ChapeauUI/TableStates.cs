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
    public partial class TableStates : ChapeauUI.BaseForm
    {
        TableService tbSrv;
        Employee user;
        List<Table> tables;
        public TableStates(Employee user)
        {
            InitializeComponent();
            tbSrv = new TableService();
            this.user = user;
        }

        private void Orders_Tables_Load(object sender, EventArgs e)
        {
            tables = tbSrv.GetAllTables(); // fill the tables list on form load
            InitiateTableStates(tables);
            

        }
        
        private List<Label> MakeLabelList()
        {
            List<Label> labels = new List<Label>();
            labels.Add(lblTb1);
            labels.Add(lblTb2);
            labels.Add(lblTb3);
            labels.Add(lblTb4);
            labels.Add(lblTb5);
            labels.Add(lblTb6);
            labels.Add(lblTb7);
            labels.Add(lblTb8);
            labels.Add(lblTb9);
            labels.Add(lblTb10);
            return labels;

        }
        private List<Button> MakeButtonList()
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(btnTable1);
            buttons.Add(btnTable2);
            buttons.Add(btnTable3);
            buttons.Add(btnTable4);
            buttons.Add(btnTable5);
            buttons.Add(btnTable6);
            buttons.Add(btnTable7);
            buttons.Add(btnTable8);
            buttons.Add(btnTable9);
            buttons.Add(btnTable10);
            return buttons;
        }
        private void InitiateTableStates(List<Table> tables)
        {
            List<Label> labels = MakeLabelList();
            //List<Button> buttons = MakeButtonList();

            for (int i = 0; i < tables.Count; i++)
            {
                if(tables[i].State == TableState.Free)
                {
                    labels[i].BackColor = Color.FromArgb(152, 251, 152);
                }
                else if(tables[i].State == TableState.Reserved)
                {
                    labels[i].BackColor = Color.FromArgb(220, 20, 60);
                }
                else if(tables[i].State == TableState.OrderTaken)
                {
                    labels[i].BackColor = Color.FromArgb(63, 224, 208);
                }
                else
                {
                    labels[i].BackColor = Color.FromArgb(191, 191, 191);
                }
            }


        }
        


        private void btnTable1_Click(object sender, EventArgs e)
        {
            tables = tbSrv.GetAllTables(); // the table list will be updated on each buttons click
            TablesClicked(tables[0]);

        }
        private void TablesClicked(Table table) //pop up menu (4 actions(occupied,served......etc ) ) // contextMenu 
        {

            
            DialogBox dialog = new DialogBox(table);

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                MenuCardSelection selection = new MenuCardSelection(DateTime.Now, user);
                DialogResult sel = selection.ShowDialog();
                TakeOrder order = new TakeOrder(this, selection.ReturnValue, user, table);
                this.Hide();
                order.ShowDialog();

            }
            else if(result == DialogResult.Yes) // reserve table
            {
                int rows = tbSrv.ChangeTableStatus(table, TableState.Reserved);

                if(rows > 0)
                {
                    MessageBox.Show("table is reserved");
                }
                else if(rows == -1)
                {
                    MessageBox.Show("database connection failed");
                }
                else
                {
                    MessageBox.Show("an error occured");
                }

            }
            else if(result == DialogResult.No) // free table
            {
                int rows = tbSrv.ChangeTableStatus(table, TableState.Free);

                if (rows > 0)
                {
                    MessageBox.Show("table is free");
                }
                else if (rows == -1)
                {
                    MessageBox.Show("database connection failed");
                }
                else
                {
                    MessageBox.Show("an error occured");
                }
            }
            tables = tbSrv.GetAllTables();
            InitiateTableStates(tables);


        }

        private void btnTable2_Click(object sender, EventArgs e)
        {
            tables = tbSrv.GetAllTables();
            TablesClicked(tables[1]);

        }

        private void btnTable3_Click(object sender, EventArgs e)
        {
            tables = tbSrv.GetAllTables();
            TablesClicked(tables[2]);

        }

        private void btnTable4_Click(object sender, EventArgs e)
        {
            tables = tbSrv.GetAllTables();
            TablesClicked(tables[3]);
        }

        private void btnTable5_Click(object sender, EventArgs e)
        {
            tables = tbSrv.GetAllTables();
            TablesClicked(tables[4]);

        }

        private void btnTable6_Click(object sender, EventArgs e)
        {
            tables = tbSrv.GetAllTables();
            TablesClicked(tables[5]);

        }

        private void btnTable7_Click(object sender, EventArgs e)
        {
            tables = tbSrv.GetAllTables();
            TablesClicked(tables[6]);

        }

        private void btnTable8_Click(object sender, EventArgs e)
        {
            tables = tbSrv.GetAllTables();
            TablesClicked(tables[7]);

        }

        private void btnTable9_Click(object sender, EventArgs e)
        {
            tables = tbSrv.GetAllTables();
            TablesClicked(tables[8]);

        }

        private void btnTable10_Click(object sender, EventArgs e)
        {
            tables = tbSrv.GetAllTables();
            TablesClicked(tables[9]);

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Orders_Tables_Load(sender, e);
        }
    }
}
