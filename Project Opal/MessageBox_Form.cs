using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Opal
{
    public partial class MessageBox_Form : Form
    {

        public MessageBox_Form(string message, string caption)
        {
            InitializeComponent();

            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 246, Screen.PrimaryScreen.Bounds.Height - 91);
            this.Text = caption;
            this.lblMessage.Text = message;
            this.Visible = true;
        }

        public static MessageBox_Form Show(string message, string caption)
        {
            return new MessageBox_Form(message, caption);
        }
    }
}
