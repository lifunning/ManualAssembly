using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManualAssembly
{
    public partial class Access : Form
    {
        public bool isOK;
        public Access()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPSW.Text == "")
            {
                this.Close();
                isOK = true;
            }
            else
            {
                MessageBox.Show("密码不正确，请重新输入！");
                txtPSW.Text = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isOK = false;
            this.Close();
        }
    }
}
