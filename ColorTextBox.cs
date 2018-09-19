using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace LabControls
{
    public enum CalculusType { Dec, Hex };

    public partial class ColorTextBox : TextBox
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
                CheckText();
                calculus = value;
                ConvertValue(value);
            }
        }

        public int GetIntValue()
        {
            switch (calculus)
            {
                case CalculusType.Dec:
                    return Convert.ToInt32(Text);
                case CalculusType.Hex:
                    return Convert.ToInt32(Text, 16);
                default:
                    return 0;
            }
        }

        private void CheckText()
        {
            if (Text == "")
                Text = "0";
        }

        protected void ConvertValue(CalculusType value)
        {
            switch (value)
            {
                case CalculusType.Dec:
                    Text = ConvertToDec(Text);
                    break;
                case CalculusType.Hex:
                    Text = ConvertToHex(Text);
                    break;
            }
        }

        private string ConvertToDec(string value)
        {
            return "" + Convert.ToInt32(value, 16);
        }

        private string ConvertToHex(string value)
        {
            return Convert.ToInt32(value).ToString("X");
        }

        public ColorTextBox()
        {
            InitializeComponent();
        }


        public ColorTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            switch (calculus)
            {
                case CalculusType.Hex:
                    if (!char.IsDigit(e.KeyChar) && !"ABCDEF".Contains(e.KeyChar) && !char.IsControl(e.KeyChar))
                        e.Handled = true;
                    base.OnKeyPress(e);
                    break;
                case CalculusType.Dec:
                    if (!char.IsDigit(e.KeyChar) && !char.IsControl((e.KeyChar)))
                        e.Handled = true;
                    base.OnKeyPress(e);
                    break;
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            CheckText();
            switch (calculus)
            {
                case CalculusType.Dec:
                    if (GetIntValue() > 255)
                        Text = "255";
                    break;
                case CalculusType.Hex:
                    if (GetIntValue() > 255)
                        Text = "FF";
                    break;
            }
            if (Text.Length > 1)
                Text = Text.TrimStart('0');
            base.OnTextChanged(e);
        }
    }
}
