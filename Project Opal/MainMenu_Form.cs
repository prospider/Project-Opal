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
        private bool shiftOpened;

        public MainMenu_Form(User u)
        {
            InitializeComponent();
            currentUser = u;
            currentShift = u.GetOpenShift();
            InitializeFormElements();
        }

        private void InitializeFormElements()
        {
            if (currentShift != null)
            {
                btnClockToClockOut();
            }
            else
            {
                btnClockToClockIn();            
            }
        }

        private void btnClockToClockOut()
        {
            btnClock.Text = "Clock out";
            lblShiftInformation.Text = String.Format("You have an open shift started at: {0}", currentShift.startTime.ToString());
        }

        private void btnClockToClockIn()
        {
            btnClock.Text = "Clock in";
            lblShiftInformation.Text = "";
        }
        
        private void btnClock_Click(object sender, EventArgs e)
        {
            if (currentShift != null)
            {
                currentUser.ClockOut(currentShift);
                btnClockToClockIn();
            }
            else
            {
                currentShift = currentUser.ClockIn(1); //TODO: Get input for vehicle number
                btnClockToClockOut();
            }
        }
    }
}
