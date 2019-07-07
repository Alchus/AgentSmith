using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmithUI
{
    public partial class HumanPlayer : Form
    {
        public Dictionary<Button, int> dict = new Dictionary<Button, int>();
        public HumanPlayer()
        {
            InitializeComponent();
        }

        public void ClearText()
        {
            richTextBox1.Text = "";
        }
        public void Write(object o)
        {
            richTextBox1.Text += o;
        }
        public void WriteLine(object o)
        {
            richTextBox1.Text += o + Environment.NewLine;
        }

        public void AddOption(int ID, string optionText)
        {
            var button = new Button {Text = optionText};
            dict.Add(button, ID);
            button.Enabled = false;
            flowLayoutPanel1.Controls.Add(button);
        }

        public void ClearOptions()
        {
            dict.Clear();
            flowLayoutPanel1.Controls.Clear();
        }
        public int GetOption()
        {
            foreach (var button in dict.Keys)
            {
                button.Enabled = true;
                button.Click += OptionButton_Click;
            }

            selection = int.MaxValue;
            while (selection == int.MaxValue)
            {
                Thread.Sleep(50);
            }

            return selection;
        }

        int selection = int.MaxValue;


        private void OptionButton_Click(object sender, EventArgs e)
        {
            selection = dict[(Button)sender];
        }
    }
}
