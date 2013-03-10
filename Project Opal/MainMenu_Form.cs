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
            btnClock.Font = new Font(btnClock.Font.FontFamily, 24);
            currentUser = u;
            currentShift = u.GetOpenShift();
            InitializeFormElements();
        }

/*        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            LogOutAndReopen("");

            e.Cancel = true;
        }
*/
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

        private void LogOutAndReopen(string msg)
        {
            MessageBox_Form alertBox = MessageBox_Form.Show(msg, "Message");

            Login_Form loginForm = new Login_Form();

            this.Visible = false;

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                alertBox.Close();
                currentUser = loginForm.currentUser;
                currentShift = currentUser.GetOpenShift();
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
            string logOutReason;

            if (currentShift != null)
            {
                currentUser.ClockOut(currentShift);
                logOutReason = "You have successfully clocked out.";
            }
            else
            {

                currentShift = currentUser.ClockIn(1); //TODO: Get input for vehicle number
                logOutReason = "You have successfully clocked in.";
            }

            LogOutAndReopen(logOutReason);
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            Form reviewShifts = new ReviewShifts_Form(currentUser.PreviousShifts());
            reviewShifts.ShowDialog();
            //Shift[] previousShifts = currentUser.PreviousShifts(currentUser);
            //lblShiftInformation.Text = previousShifts[3].startTime.ToString() + " - " + previousShifts[3].endTime.ToString();
        }
    }
}
