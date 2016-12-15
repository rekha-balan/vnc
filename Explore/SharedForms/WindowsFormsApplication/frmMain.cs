using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedForms;

namespace WindowsFormsApplication
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnForm1_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            form.Show();
        }

        private void btnForm2_Click(object sender, EventArgs e)
        {
            var form = new Form2();
            form.Show();
        }

        private void btnForm1SH_Click(object sender, EventArgs e)
        {
            var form = new SharedForms.Form1();
            form.Show();
        }

        private void btnForm2SH_Click(object sender, EventArgs e)
        {
            var form = new SharedForms.Form2();
            form.Show();
        }
    }
}
