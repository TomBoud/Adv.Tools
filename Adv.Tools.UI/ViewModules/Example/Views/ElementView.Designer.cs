namespace Adv.Tools.UI.Example.Views
{
    partial class ElementView
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
            this.ElementDisplay_dgv = new System.Windows.Forms.DataGridView();
            this.SearchElement_txb = new System.Windows.Forms.TextBox();
            this.Delete_btn = new System.Windows.Forms.Button();
            this.Show_btn = new System.Windows.Forms.Button();
            this.Search_btn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Search_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ElementDisplay_dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ElementDisplay_dgv
            // 
            this.ElementDisplay_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ElementDisplay_dgv.Location = new System.Drawing.Point(3, 56);
            this.ElementDisplay_dgv.Name = "ElementDisplay_dgv";
            this.ElementDisplay_dgv.Size = new System.Drawing.Size(612, 316);
            this.ElementDisplay_dgv.TabIndex = 2;
            // 
            // SearchElement_txb
            // 
            this.SearchElement_txb.Location = new System.Drawing.Point(3, 30);
            this.SearchElement_txb.Name = "SearchElement_txb";
            this.SearchElement_txb.Size = new System.Drawing.Size(508, 20);
            this.SearchElement_txb.TabIndex = 3;
            // 
            // Delete_btn
            // 
            this.Delete_btn.Location = new System.Drawing.Point(621, 56);
            this.Delete_btn.Name = "Delete_btn";
            this.Delete_btn.Size = new System.Drawing.Size(104, 37);
            this.Delete_btn.TabIndex = 4;
            this.Delete_btn.Text = "Delete";
            this.Delete_btn.UseVisualStyleBackColor = true;
            // 
            // Show_btn
            // 
            this.Show_btn.Location = new System.Drawing.Point(621, 99);
            this.Show_btn.Name = "Show_btn";
            this.Show_btn.Size = new System.Drawing.Size(104, 37);
            this.Show_btn.TabIndex = 5;
            this.Show_btn.Text = "Show Selected";
            this.Show_btn.UseVisualStyleBackColor = true;
            // 
            // Search_btn
            // 
            this.Search_btn.Location = new System.Drawing.Point(517, 30);
            this.Search_btn.Name = "Search_btn";
            this.Search_btn.Size = new System.Drawing.Size(98, 20);
            this.Search_btn.TabIndex = 6;
            this.Search_btn.Text = "Search";
            this.Search_btn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Search_lbl);
            this.panel1.Controls.Add(this.ElementDisplay_dgv);
            this.panel1.Controls.Add(this.Search_btn);
            this.panel1.Controls.Add(this.SearchElement_txb);
            this.panel1.Controls.Add(this.Show_btn);
            this.panel1.Controls.Add(this.Delete_btn);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(732, 375);
            this.panel1.TabIndex = 7;
            // 
            // Search_lbl
            // 
            this.Search_lbl.AutoSize = true;
            this.Search_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Search_lbl.Location = new System.Drawing.Point(3, 7);
            this.Search_lbl.Name = "Search_lbl";
            this.Search_lbl.Size = new System.Drawing.Size(137, 20);
            this.Search_lbl.TabIndex = 8;
            this.Search_lbl.Text = "Search Element";
            // 
            // ElementView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 399);
            this.Controls.Add(this.panel1);
            this.Name = "ElementView";
            this.Text = "ElementViewForm";
            ((System.ComponentModel.ISupportInitialize)(this.ElementDisplay_dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView ElementDisplay_dgv;
        private System.Windows.Forms.TextBox SearchElement_txb;
        private System.Windows.Forms.Button Delete_btn;
        private System.Windows.Forms.Button Show_btn;
        private System.Windows.Forms.Button Search_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Search_lbl;
    }
}