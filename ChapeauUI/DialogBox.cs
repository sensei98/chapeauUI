using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class DialogBox : Form
    {
        public string ReturnValue { get; private set; }
        Table table;
        public DialogBox(Table table)
        {
            InitializeComponent();
            this.table = table;
            FillForm();

        }
        

        private void btnTake_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "Take Order";
            this.DialogResult = DialogResult.OK; // ok for taking orders

            this.Close();



        }
        private void FillForm()
        {
            this.Name = "Table #" + table.TableID.ToString();
            lblTable.Text = "Table #" + table.TableID.ToString();
            lblstat.Text = "Table status: " + table.State.ToString();
            if(table.State == TableState.Free)
            {
                btnFree.Enabled = false;
                btnFree.BackColor = Color.FromArgb(147, 165, 172);
                
                
                
            }
            else if(table.State == TableState.OrderServed || table.State == TableState.OrderTaken)
            {

                btnReserve.Enabled = false;
                btnReserve.BackColor = Color.FromArgb(147, 165, 172);

            }
        
        }

        private void DialogBox_Load(object sender, EventArgs e)
        {

        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            ReturnValue = "Reserve";

            this.DialogResult = DialogResult.Yes; // yes for reserve

            this.Close();

        }

        private void btnFree_Click(object sender, EventArgs e)
        {
            ReturnValue = "Free";
            this.DialogResult = DialogResult.No; // no for free

            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnValue = "Cancel";
            this.DialogResult = DialogResult.Cancel;

            this.Close();

        }
    }
}
