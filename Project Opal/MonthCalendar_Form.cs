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
    public partial class MonthCalendar_Form : Form
    {
        DataTable shifts;
        private User currentUser;
        int userId;
        DateTime selectedDate;
        public MonthCalendar_Form(DataTable _shifts, User u)
        {
            InitializeComponent();
            //shifts = _shifts;
            currentUser = u;
            //Bolds the numbers for days that have shifts starting on them
            int numShifts = _shifts.Rows.Count;
            
            DateTime[] shiftArray = new DateTime[numShifts];
            for (int i = 0; i < numShifts; i++)
            {
                shiftArray[i] = Convert.ToDateTime(_shifts.Rows[i][3]);
                if(i == numShifts-1)
                    userId = Convert.ToInt32(_shifts.Rows[i][1]);
            }
            this.monthCalendar1.BoldedDates = shiftArray;
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
        }

        private void monthCalendar1_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {

            selectedDate = e.Start;
            shifts = currentUser.SelectedShifts(e.Start);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form reviewShifts = new ReviewShifts_Form(shifts);
            //this.Hide();
            //reviewShifts.ShowDialog();
        }
    }
}
