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
    public partial class MainMenu_Form : Form
    {
        private User currentUser;
        private Shift currentShift;

        public MainMenu_Form(User u)
        {
            InitializeComponent();
            currentUser = u;
            InitializeFormElements();
        }

        private void InitializeFormElements()
        {
            DatabaseConnection con = new DatabaseConnection(DatabaseConnection.DATABASE_LOG);
            con.Open();

            var openShiftStartDate = con.ExecuteScalar(String.Format("SELECT MAX(start_time) FROM 'T_SHIFT' WHERE employee_id = {0} AND end_time IS NULL",
                currentUser.id.ToString()));

            if (openShiftStartDate != null)
            {
                lblShiftInformation.Text = String.Format("You have an open shift started at: {0}", openShiftStartDate.ToString());
                btnClock.Text = "Clock out";
                //btnClock.Click += currentShift.ClockOut();
            }
            else
            {
                lblShiftInformation.Text = "";
                btnClock.Text = "Clock in";
                //btnClock.Click += Shift.ClockIn(currentUser.id, 1);
            }

            con.Close();
        }
    }
}
