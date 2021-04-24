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
    public partial class MenuCardSelection : Form
    {
        public string ReturnValue { get; private set; } // in case its ever needed to check the return value
        private DateTime time;
        
        public MenuCardSelection(DateTime time, Employee user)
        {
            InitializeComponent();
            this.time = time;
            
            InitializeForm();
        }
        private void InitializeForm()
        {
            List<Button> lunchMenuButtons = new List<Button> { btnLunchMain, btnLunchBites, btnLunchSpecials };
            List<Button> dinnerMenuButtons = new List<Button> { btnDinnerDesserts, btnDinnerMain, btnDinnerStarters };
            if(time.Hour > 15)
            {
                foreach(Button btn in lunchMenuButtons)
                {
                    btn.Enabled = false;
                    btn.BackColor = Color.FromArgb(147, 165, 172);
                }
            }
            else
            {
                foreach(Button btn in dinnerMenuButtons)
                {
                    btn.Enabled = false;
                    btn.BackColor = Color.FromArgb(147, 165, 172);
                }
            }
        }

        private void btnLunchMain_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "LunchMain";
            

            this.Close();
        }

        private void btnLunchSpecials_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "LunchSpecials";
            

            this.Close();
        }

        private void btnLunchBites_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "LunchBites";
            

            this.Close();
        }

        private void btnDinnerStarters_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "DinnerStarters";
            

            this.Close();
        }

        private void btnDinnerMain_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "DinnerMains";
            

            this.Close();
        }

        private void btnDinnerDesserts_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "DinnerDesserts";
            

            this.Close();
        }

        private void btnSoftDrinks_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "SoftDrinks";
            

            this.Close();
        }

        private void btnHotDrinks_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "HotDrinks";
            

            this.Close();
        }

        private void btnBeers_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "Beers";
            

            this.Close();
        }

        private void btnWines_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "Wines";
            

            this.Close();
        }

        
    }
}
