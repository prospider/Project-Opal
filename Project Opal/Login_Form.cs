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
        public User currentUser;

        public Login_Form()
        {
            InitializeComponent();
            log = new Logger("Loginlog.txt");
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
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.ForeColor != Color.Black) { txtUsername.ForeColor = Color.Black; }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.ForeColor != Color.Black) { txtPassword.ForeColor = Color.Black; }
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User current = User.Login(txtUsername.Text.ToString(), txtPassword.Text.ToString());

            if(current != null)
            {
                currentUser = current;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Oops! We didn't recognize that username/password. Please try again.");
                txtPassword.Text = "";
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}
