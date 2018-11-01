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
    public partial class Screwdriver1 : Form
    {
        public Screwdriver1()
        {
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            BasicLib.Instance.Screw1Port = cboScrew1Port.Text;
            BasicLib.Instance.Screw1Bautrate = Convert.ToInt32(cboScrew1Baut.Text);
            BasicLib.Instance.Screw1Parity = Convert.ToInt16(cboScrew1Parity.SelectedIndex);
            BasicLib.Instance.Screw1StopBit = Convert.ToInt16(cboScrew1Stop.Text);
            BasicLib.Instance.Screw1DataBit = Convert.ToInt16(cboScrew1Data.Text);
            //BasicLib.Instance.TorqueUnit = cboTorqueUnit.Text;
            BasicLib.Instance.Write();
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Screwdriver1_Load(object sender, EventArgs e)
        {
            cboScrew1Port.Text = BasicLib.Instance.Screw1Port;
            cboScrew1Baut.Text = BasicLib.Instance.Screw1Bautrate.ToString();
            cboScrew1Parity.SelectedIndex = BasicLib.Instance.Screw1Parity;
            cboScrew1Data.Text = BasicLib.Instance.Screw1DataBit.ToString();
            cboScrew1Stop.Text = BasicLib.Instance.Screw1StopBit.ToString();
            //cboTorqueUnit.Text = BasicLib.Instance.TorqueUnit;
        }
    }
}
