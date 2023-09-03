namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views
{
    partial class ConfigExpectedMainView
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
            this.grids_button = new System.Windows.Forms.Button();
            this.models_button = new System.Windows.Forms.Button();
            this.worksets_button = new System.Windows.Forms.Button();
            this.levels_button = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.levels_button);
            this.panel1.Controls.Add(this.grids_button);
            this.panel1.Controls.Add(this.models_button);
            this.panel1.Controls.Add(this.worksets_button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 594);
            this.panel1.TabIndex = 0;
            // 
            // grids_button
            // 
            this.grids_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grids_button.Location = new System.Drawing.Point(3, 92);
            this.grids_button.Name = "grids_button";
            this.grids_button.Size = new System.Drawing.Size(138, 34);
            this.grids_button.TabIndex = 2;
            this.grids_button.Text = "Grids";
            this.grids_button.UseVisualStyleBackColor = true;
            // 
            // models_button
            // 
            this.models_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.models_button.Location = new System.Drawing.Point(3, 12);
            this.models_button.Name = "models_button";
            this.models_button.Size = new System.Drawing.Size(138, 34);
            this.models_button.TabIndex = 1;
            this.models_button.Text = "Models";
            this.models_button.UseVisualStyleBackColor = true;
            // 
            // worksets_button
            // 
            this.worksets_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.worksets_button.Location = new System.Drawing.Point(3, 52);
            this.worksets_button.Name = "worksets_button";
            this.worksets_button.Size = new System.Drawing.Size(138, 34);
            this.worksets_button.TabIndex = 0;
            this.worksets_button.Text = "Worksets";
            this.worksets_button.UseVisualStyleBackColor = true;
            // 
            // levels_button
            // 
            this.levels_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.levels_button.Location = new System.Drawing.Point(3, 132);
            this.levels_button.Name = "levels_button";
            this.levels_button.Size = new System.Drawing.Size(138, 34);
            this.levels_button.TabIndex = 3;
            this.levels_button.Text = "Levels";
            this.levels_button.UseVisualStyleBackColor = true;
            // 
            // ConfigExpectedMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 594);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Name = "ConfigExpectedMainView";
            this.Text = "ConfigExpectedMainView";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button worksets_button;
        private System.Windows.Forms.Button models_button;
        private System.Windows.Forms.Button grids_button;
        private System.Windows.Forms.Button levels_button;
    }
}