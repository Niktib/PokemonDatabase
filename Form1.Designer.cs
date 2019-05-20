namespace PokemonDatabase
{
    partial class Form1
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
            this.Download_Set_Names = new System.Windows.Forms.Button();
            this.Get_Card_Names = new System.Windows.Forms.Button();
            this.TCG = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // Download_Set_Names
            // 
            this.Download_Set_Names.Location = new System.Drawing.Point(12, 35);
            this.Download_Set_Names.Name = "Download_Set_Names";
            this.Download_Set_Names.Size = new System.Drawing.Size(190, 39);
            this.Download_Set_Names.TabIndex = 0;
            this.Download_Set_Names.Text = "Get Sets";
            this.Download_Set_Names.UseVisualStyleBackColor = true;
            this.Download_Set_Names.Click += new System.EventHandler(this.Download_Set_Names_Click);
            // 
            // Get_Card_Names
            // 
            this.Get_Card_Names.Location = new System.Drawing.Point(12, 80);
            this.Get_Card_Names.Name = "Get_Card_Names";
            this.Get_Card_Names.Size = new System.Drawing.Size(190, 38);
            this.Get_Card_Names.TabIndex = 2;
            this.Get_Card_Names.Text = "Get Cards";
            this.Get_Card_Names.UseVisualStyleBackColor = true;
            this.Get_Card_Names.Click += new System.EventHandler(this.Get_Card_Names_Click);
            // 
            // TCG
            // 
            this.TCG.AutoSize = true;
            this.TCG.Location = new System.Drawing.Point(86, 12);
            this.TCG.Name = "TCG";
            this.TCG.Size = new System.Drawing.Size(47, 17);
            this.TCG.TabIndex = 3;
            this.TCG.TabStop = true;
            this.TCG.Text = "TCG";
            this.TCG.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 126);
            this.Controls.Add(this.TCG);
            this.Controls.Add(this.Get_Card_Names);
            this.Controls.Add(this.Download_Set_Names);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Download_Set_Names;
        private System.Windows.Forms.Button Get_Card_Names;
        private System.Windows.Forms.RadioButton TCG;
    }
}

