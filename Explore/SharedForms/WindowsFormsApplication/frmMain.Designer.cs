namespace WindowsFormsApplication
{
    partial class frmMain
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
            this.btnForm1 = new System.Windows.Forms.Button();
            this.btnForm2 = new System.Windows.Forms.Button();
            this.btnForm1SH = new System.Windows.Forms.Button();
            this.btnForm2SH = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnForm1
            // 
            this.btnForm1.Location = new System.Drawing.Point(89, 35);
            this.btnForm1.Name = "btnForm1";
            this.btnForm1.Size = new System.Drawing.Size(124, 53);
            this.btnForm1.TabIndex = 0;
            this.btnForm1.Text = "Form1";
            this.btnForm1.UseVisualStyleBackColor = true;
            this.btnForm1.Click += new System.EventHandler(this.btnForm1_Click);
            // 
            // btnForm2
            // 
            this.btnForm2.Location = new System.Drawing.Point(89, 103);
            this.btnForm2.Name = "btnForm2";
            this.btnForm2.Size = new System.Drawing.Size(124, 49);
            this.btnForm2.TabIndex = 1;
            this.btnForm2.Text = "Form2";
            this.btnForm2.UseVisualStyleBackColor = true;
            this.btnForm2.Click += new System.EventHandler(this.btnForm2_Click);
            // 
            // btnForm1SH
            // 
            this.btnForm1SH.Location = new System.Drawing.Point(306, 35);
            this.btnForm1SH.Name = "btnForm1SH";
            this.btnForm1SH.Size = new System.Drawing.Size(124, 53);
            this.btnForm1SH.TabIndex = 2;
            this.btnForm1SH.Text = "Form1 SH";
            this.btnForm1SH.UseVisualStyleBackColor = true;
            this.btnForm1SH.Click += new System.EventHandler(this.btnForm1SH_Click);
            // 
            // btnForm2SH
            // 
            this.btnForm2SH.Location = new System.Drawing.Point(306, 103);
            this.btnForm2SH.Name = "btnForm2SH";
            this.btnForm2SH.Size = new System.Drawing.Size(124, 49);
            this.btnForm2SH.TabIndex = 3;
            this.btnForm2SH.Text = "Form2 SH";
            this.btnForm2SH.UseVisualStyleBackColor = true;
            this.btnForm2SH.Click += new System.EventHandler(this.btnForm2SH_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 229);
            this.Controls.Add(this.btnForm2SH);
            this.Controls.Add(this.btnForm1SH);
            this.Controls.Add(this.btnForm2);
            this.Controls.Add(this.btnForm1);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnForm1;
        private System.Windows.Forms.Button btnForm2;
        private System.Windows.Forms.Button btnForm1SH;
        private System.Windows.Forms.Button btnForm2SH;
    }
}