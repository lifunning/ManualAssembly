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
    public partial class Screwdriver3 : Form
    {
        public Screwdriver3()
        {
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            BasicLib.Instance.Screw3Port = cboScrew3Port.Text;
            BasicLib.Instance.Screw3Bautrate = Convert.ToInt32(cboScrew3Baut.Text);
            BasicLib.Instance.Screw3Parity = Convert.ToInt16(cboScrew3Parity.SelectedIndex);
            BasicLib.Instance.Screw3StopBit = Convert.ToInt16(cboScrew3Stop.Text);
            BasicLib.Instance.Screw3DataBit = Convert.ToInt16(cboScrew3Data.Text);
            //BasicLib.Instance.TorqueUnit = cboTorqueUnit.Text;
            BasicLib.Instance.Write();
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Screwdriver3_Load(object sender, EventArgs e)
        {
            cboScrew3Port.Text = BasicLib.Instance.Screw3Port;
            cboScrew3Baut.Text = BasicLib.Instance.Screw3Bautrate.ToString();
            cboScrew3Parity.SelectedIndex = BasicLib.Instance.Screw3Parity;
            cboScrew3Data.Text = BasicLib.Instance.Screw3DataBit.ToString();
            cboScrew3Stop.Text = BasicLib.Instance.Screw3StopBit.ToString();
        }
    }
}