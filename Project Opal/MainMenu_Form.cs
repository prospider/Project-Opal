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
        private readonly Size DEFAULT_SIZE = new Size(300, 214);
        private readonly Size REVIEW_SHIFTS_SIZE = new Size(300, 269);
        private DataTable LAST_SHIFT = null;

        public MainMenu_Form(User u)
        {
            InitializeComponent();
            btnClock.Font = new Font(btnClock.Font.FontFamily, 24);
            currentUser = u;
            currentShift = u.GetOpenShift();
            InitializeFormElements();
            this.Size = DEFAULT_SIZE;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            LogOutAndReopen();

            e.Cancel = true;
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

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                currentUser = loginForm.currentUser;
                currentShift = currentUser.GetOpenShift();
                LAST_SHIFT = null;
                InitializeFormElements();
                this.Visible = true;
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

            if (LAST_SHIFT == null)
            {
                LAST_SHIFT = currentUser.LastShift();
            }

            lblLastShiftInformation.Text = String.Format("Started: {0} {1} Ended: {2}", LAST_SHIFT.Rows[0][0].ToString(), Environment.NewLine, LAST_SHIFT.Rows[0][1].ToString());
            //Form reviewShifts = new ReviewShifts_Form(currentUser.PreviousShifts());
            //reviewShifts.ShowDialog();
            //Shift[] previousShifts = currentUser.PreviousShifts(currentUser);
            //lblShiftInformation.Text = previousShifts[3].startTime.ToString() + " - " + previousShifts[3].endTime.ToString();
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
    }
}
