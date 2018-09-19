using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabControls
{
    public partial class ColorSelect : UserControl
    {
        private CalculusType calculus = CalculusType.Dec;

        public CalculusType Calculus
        {
            get
            {
                return calculus;
            }
            set
            {
                calculus = value;
                cbRed.Calculus = value;
                cbGreen.Calculus = value;
                cbBlue.Calculus = value;
            }
        }
        public ColorSelect()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void rbHex_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHex.Checked)
                Calculus = CalculusType.Hex;
        }

        private void rbDec_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDec.Checked)
                Calculus = CalculusType.Dec;
        }

        private void cbRed_TextChanged(object sender, EventArgs e)
        {
            btQuad.BackColor = Color.FromArgb(cbRed.GetIntValue(), cbGreen.GetIntValue(), cbBlue.GetIntValue());
        }

        private void cbGreen_TextChanged(object sender, EventArgs e)
        {
            btQuad.BackColor = Color.FromArgb(cbRed.GetIntValue(), cbGreen.GetIntValue(), cbBlue.GetIntValue());
        }

        private void cbBlue_TextChanged(object sender, EventArgs e)
        {
            btQuad.BackColor = Color.FromArgb(cbRed.GetIntValue(), cbGreen.GetIntValue(), cbBlue.GetIntValue());
        }
    }
}
