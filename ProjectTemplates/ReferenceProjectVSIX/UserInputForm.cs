using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReferenceProjectVSIX
{
    public partial class UserInputForm : Form
    {
        static string customMessage;
        readonly TextBox textBox1;
        readonly Button button1;

        public UserInputForm()
        {
            Size = new Size(155, 265);

            button1 = new Button();
            button1.Location = new Point(90, 25);
            button1.Size = new Size(50, 25);
            button1.Click += button1_Click;
            Controls.Add(button1);

            textBox1 = new TextBox();
            textBox1.Location = new Point(10, 25);
            textBox1.Size = new Size(70, 20);
            Controls.Add(textBox1);
        }
        public static string CustomMessage
        {
            get
            {
                return customMessage;
            }
            set
            {
                customMessage = value;
            }
        }
        void button1_Click(object sender, EventArgs e)
        {
            customMessage = textBox1.Text;
            Close();
        }
    }
}
