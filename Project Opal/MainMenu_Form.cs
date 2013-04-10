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
        private DataTable lastShift = null;
        private readonly Size DEFAULT_SIZE = new Size(356, 214);
        private readonly Size REVIEW_SHIFTS_SIZE = new Size(356, 269);
        private bool mainMenuOpen = true;

        public MainMenu_Form(User u)
        {
            InitializeComponent();
            currentUser = u;
            currentShift = u.GetOpenShift();
            InitializeFormElements();
            this.Size = DEFAULT_SIZE;
            numVehicle.Value = currentUser.LastVehicleUsed();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (mainMenuOpen)
            {
                LogOutAndReopen();
                e.Cancel = true;
            }
        }

        private void InitializeFormElements()
        {
            if (currentShift != null)
            {
                ChangeBtnClock("Clock out", String.Format("You have an open shift started at: {0}", currentShift.startTime.ToString()));
            }
            else
            {
                ChangeBtnClock("Clock in", "");
            }
        }

        private void LogOutAndReopen()
        {
            Login_Form loginForm = new Login_Form();

            this.Visible = false;
            mainMenuOpen = false;

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                currentUser = loginForm.currentUser;
                currentShift = currentUser.GetOpenShift();
                lastShift = null;
                InitializeFormElements();
                this.Visible = true;
                mainMenuOpen = true;
            }
            else
            {
                Application.Exit();
            }
        }

        private void ChangeBtnClock(string buttonText, string shiftInformationText)
        {
            btnClock.Text = buttonText;
            lblShiftInformation.Text = shiftInformationText;
        }
        
        private void btnClock_Click(object sender, EventArgs e)
        {
            if (currentShift != null)
            {
                currentUser.ClockOut(currentShift);
            }
            else
            {

                currentShift = currentUser.ClockIn(1); //TODO: Get input for vehicle number
            }

            LogOutAndReopen();
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            this.Size = REVIEW_SHIFTS_SIZE;
            lblLastShift.Visible = true;
            lblLastShiftInformation.Visible = true;
            btnCloseReviewShifts.Visible = true;
            btnMoreShiftInformation.Visible = true;
            btnReview.Visible = false;

            if (lastShift == null)
            {
                lastShift = currentUser.LastShift();
            }

            lblLastShiftInformation.Text = String.Format("Started: {0} {1} Ended: {2}", lastShift.Rows[0][0].ToString(), Environment.NewLine, lastShift.Rows[0][1].ToString());
        }

        private void btnCloseReviewShifts_Click(object sender, EventArgs e)
        {
            this.Size = DEFAULT_SIZE;
            lblLastShift.Visible = false;
            lblLastShiftInformation.Visible = false;
            btnCloseReviewShifts.Visible = false;
            btnMoreShiftInformation.Visible = false;
            btnReview.Visible = true;
        }

        private void btnMoreShiftInformation_Click(object sender, EventArgs e)
        {
            ReviewShifts_Form ReviewShiftsForm = new ReviewShifts_Form(currentUser.PreviousShifts(), this.Location.X + this.Width, this.Location.Y);
            ReviewShiftsForm.ShowDialog();
        }

        private void chkVehicleLocked_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVehicleLocked.Checked)
            {
                numVehicle.Enabled = false;
            }
            else
            {
                numVehicle.Enabled = true;
            }
        }
    }
}
