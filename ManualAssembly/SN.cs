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
    public partial class SN : Form
    {
        public bool isOK;
        public SN()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hasher HDid = new Hasher();
            string sn = HDid.Sern();
            if (sn == textBox1.Text)
            {
                BasicLib.Instance.SN = sn;
                isOK = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("SN is not correct");
            }
        }

        private void SN_Load(object sender, EventArgs e)
        {
            Hasher hash = new Hasher();
            txtMachineID.Text = hash.hashGetDriveID().ToString();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            isOK = false;
            this.Close();
        }
    }
}