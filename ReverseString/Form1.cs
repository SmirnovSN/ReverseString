using System;
using System.Windows.Forms;

namespace ReverseString
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            const string s1 = "poiuytrewq0987654321";
            const string s2 = "poiuytrewq0987654321";

            ReverseString.ReverseFastest(s1);

            textBox1.Text = s2; // !
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = ReverseString.ReverseUnsafeCopy(textBox1.Text);
        }
    }
}
