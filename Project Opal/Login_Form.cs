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
            log = new Logger("Loginlog.txt");
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Equals("Username"))
            {
                txtUsername.ForeColor = Color.Black;
                txtUsername.Text = "";
                log.Write("User Clicked on Username Field.");
            }
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals("Password"))
            {
                txtPassword.ForeColor = Color.Black;
                txtPassword.Text = "";
                log.Write("User Clicked on Password Field.");
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

                db = new DatabaseConnection("Loginlog.txt");
                db.Open(); //modded this until i get clarification on the purpose of Open().

                string stm = String.Format("SELECT password FROM T_USER WHERE username = '{0}'", txtUsername.Text);
                log.Write(string.Format("Sent query to Database: {0}", stm));
                var row = db.ExecuteScalar(stm);

                if (row != null)
                {
                    log.Write(String.Format("Found a result for User in SQL DB:{0}\nAssociated HASH is {1}", txtUsername.Text, row)); //might need to .toString() the row. 
                    string retrievedPassword = row.ToString();
                    string inputPassword = txtPassword.Text.ToString();
                    string hashedInputPassword = Secure.Hash(inputPassword);

                    if (retrievedPassword.Equals(hashedInputPassword))
                    {
                        log.Write(String.Format("Access Granted to User: {0}", txtUsername.Text));
                    }
                    else
                    {
                        log.Write(String.Format("Access Denied to User: {0}\n Password Attempted: {1}", txtUsername.Text, txtPassword.Text));
                    }
                }
                else
                {
                    log.Write(String.Format("Found no user in DB Named: {0}", txtUsername.Text));
                }

                db.Close();

            }
            else
            {
                // DENIED
            }
        }
    }
}
