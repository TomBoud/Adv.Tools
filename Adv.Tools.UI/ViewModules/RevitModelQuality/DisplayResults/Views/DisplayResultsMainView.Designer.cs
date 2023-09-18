namespace Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Views
{
    partial class DisplayResultsMainView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.elementsWorksets_button = new System.Windows.Forms.Button();
            this.missingWorksets_button = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.missingWorksets_button);
            this.panel1.Controls.Add(this.elementsWorksets_button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 594);
            this.panel1.TabIndex = 0;
            // 
            // elementsWorksets_button
            // 
            this.elementsWorksets_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.elementsWorksets_button.Location = new System.Drawing.Point(3, 12);
            this.elementsWorksets_button.Name = "elementsWorksets_button";
            this.elementsWorksets_button.Size = new System.Drawing.Size(244, 34);
            this.elementsWorksets_button.TabIndex = 1;
            this.elementsWorksets_button.Text = "Elements on Worksets";
            this.elementsWorksets_button.UseVisualStyleBackColor = true;
            // 
            // missingWorksets_button
            // 
            this.missingWorksets_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.missingWorksets_button.Location = new System.Drawing.Point(3, 52);
            this.missingWorksets_button.Name = "missingWorksets_button";
            this.missingWorksets_button.Size = new System.Drawing.Size(244, 34);
            this.missingWorksets_button.TabIndex = 2;
            this.missingWorksets_button.Text = "Missing Worksets";
            this.missingWorksets_button.UseVisualStyleBackColor = true;
            // 
            // DisplayResultsMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 594);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Name = "DisplayResultsMainView";
            this.Text = "DisplayResultsMainView";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button elementsWorksets_button;
        private System.Windows.Forms.Button missingWorksets_button;
    }
}