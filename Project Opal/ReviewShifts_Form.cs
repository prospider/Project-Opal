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
    public partial class ReviewShifts_Form : Form
    {
        DataTable shifts;
        private readonly int DEFAULT_DGV_WIDTH = 400;

        public ReviewShifts_Form(DataTable _shifts, int _parentx, int _parenty)
        {
            InitializeComponent();
            shifts = _shifts;
            dgvShifts.DataSource = shifts;
            dgvShifts.ReadOnly = true;
            this.Location = new Point(_parentx, _parenty);
        }

        private void dgvShifts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            /* Set-up column width that make sense */
            dgvShifts.Columns[0].Width = 35;
            dgvShifts.Columns[1].Width = 65;
            dgvShifts.Columns[2].Width = 120;
            dgvShifts.Columns[3].Width = 120;

            /* Set-up human readable column names */
            dgvShifts.Columns[1].HeaderText = "Car #";
            dgvShifts.Columns[2].HeaderText = "Start";
            dgvShifts.Columns[3].HeaderText = "End";
        }
    }
}
