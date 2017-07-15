using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using VNC.AssemblyHelper;

namespace TestAssemblyHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayAssemblyInfo();
        }
                
        private void DisplayAssemblyInfo()
        {
            Assembly asmb = Assembly.GetExecutingAssembly();
            AssemblyInformation info = new AssemblyInformation(asmb);
            txtOutput.Text = txtOutput.Text + System.Environment.NewLine + info.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            
            if (result != DialogResult.Cancel)
            {
                txtFileName.Text = openFileDialog1.FileName;            	;
                AssemblyInformation info = new AssemblyInformation(txtFileName.Text);
                txtOutput.Text = txtOutput.Text + System.Environment.NewLine + info.ToString();
            }
        }
    }
}
