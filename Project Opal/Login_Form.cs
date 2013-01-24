using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Project_Opal
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Equals("Username"))
            {
                txtUsername.ForeColor = Color.Black;
                txtUsername.Text = "";
            }
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals("Password"))
            {
                txtPassword.ForeColor = Color.Black;
                txtPassword.Text = "";
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.ForeColor != Color.Black) { txtUsername.ForeColor = Color.Black; }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.ForeColor != Color.Black) { txtPassword.ForeColor = Color.Black; }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User currentUser = User.Login(txtUsername.Text.ToString(), txtPassword.Text.ToString());

            if(currentUser != null)
            {
                // GRANTED
            }
            else
            {
                // DENIED
            }
        }
    }
}
