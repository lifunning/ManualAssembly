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
    public partial class Screwdriver2 : Form
    {
        public Screwdriver2()
        {
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            BasicLib.Instance.Screw2Port = cboScrew2Port.Text;
            BasicLib.Instance.Screw2Bautrate = Convert.ToInt32(cboScrew2Baut.Text);
            BasicLib.Instance.Screw2Parity = Convert.ToInt16(cboScrew2Parity.SelectedIndex);
            BasicLib.Instance.Screw2StopBit = Convert.ToInt16(cboScrew2Stop.Text);
            BasicLib.Instance.Screw2DataBit = Convert.ToInt16(cboScrew2Data.Text);
            //BasicLib.Instance.TorqueUnit = cboTorqueUnit.Text;
            BasicLib.Instance.Write();
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Screwdriver2_Load(object sender, EventArgs e)
        {
            cboScrew2Port.Text = BasicLib.Instance.Screw2Port;
            cboScrew2Baut.Text = BasicLib.Instance.Screw2Bautrate.ToString();
            cboScrew2Parity.SelectedIndex = BasicLib.Instance.Screw2Parity;
            cboScrew2Data.Text = BasicLib.Instance.Screw2DataBit.ToString();
            cboScrew2Stop.Text = BasicLib.Instance.Screw2StopBit.ToString();
        }
    }
}