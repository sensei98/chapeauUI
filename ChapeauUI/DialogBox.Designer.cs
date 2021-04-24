namespace ChapeauUI
{
    partial class DialogBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReserve = new System.Windows.Forms.Button();
            this.btnTake = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.lblstat = new System.Windows.Forms.Label();
            this.btnFree = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(129)))), ((int)(((byte)(132)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(339, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 37);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReserve
            // 
            this.btnReserve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(129)))), ((int)(((byte)(132)))));
            this.btnReserve.FlatAppearance.BorderSize = 0;
            this.btnReserve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReserve.ForeColor = System.Drawing.Color.White;
            this.btnReserve.Location = new System.Drawing.Point(125, 175);
            this.btnReserve.Name = "btnReserve";
            this.btnReserve.Size = new System.Drawing.Size(89, 37);
            this.btnReserve.TabIndex = 4;
            this.btnReserve.Text = "&Reserve Table";
            this.btnReserve.UseVisualStyleBackColor = false;
            this.btnReserve.Click += new System.EventHandler(this.btnReserve_Click);
            // 
            // btnTake
            // 
            this.btnTake.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(129)))), ((int)(((byte)(132)))));
            this.btnTake.FlatAppearance.BorderSize = 0;
            this.btnTake.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTake.ForeColor = System.Drawing.Color.White;
            this.btnTake.Location = new System.Drawing.Point(18, 175);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(89, 37);
            this.btnTake.TabIndex = 5;
            this.btnTake.Text = "&TakeOrder";
            this.btnTake.UseVisualStyleBackColor = false;
            this.btnTake.Click += new System.EventHandler(this.btnTake_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Please select an action to continue";
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(12, 36);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(30, 13);
            this.lblTable.TabIndex = 7;
            this.lblTable.Text = "table";
            // 
            // lblstat
            // 
            this.lblstat.AutoSize = true;
            this.lblstat.Location = new System.Drawing.Point(12, 73);
            this.lblstat.Name = "lblstat";
            this.lblstat.Size = new System.Drawing.Size(37, 13);
            this.lblstat.TabIndex = 8;
            this.lblstat.Text = "Status";
            // 
            // btnFree
            // 
            this.btnFree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(129)))), ((int)(((byte)(132)))));
            this.btnFree.FlatAppearance.BorderSize = 0;
            this.btnFree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFree.ForeColor = System.Drawing.Color.White;
            this.btnFree.Location = new System.Drawing.Point(232, 175);
            this.btnFree.Name = "btnFree";
            this.btnFree.Size = new System.Drawing.Size(89, 37);
            this.btnFree.TabIndex = 9;
            this.btnFree.Text = "&Free Table";
            this.btnFree.UseVisualStyleBackColor = false;
            this.btnFree.Click += new System.EventHandler(this.btnFree_Click);
            // 
            // DialogBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 230);
            this.Controls.Add(this.btnFree);
            this.Controls.Add(this.lblstat);
            this.Controls.Add(this.lblTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTake);
            this.Controls.Add(this.btnReserve);
            this.Controls.Add(this.btnCancel);
            this.Name = "DialogBox";
            this.Text = "DialogBox";
            this.Load += new System.EventHandler(this.DialogBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReserve;
        private System.Windows.Forms.Button btnTake;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblstat;
        private System.Windows.Forms.Button btnFree;
    }
}