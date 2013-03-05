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

        public ReviewShifts_Form(DataTable _shifts)
        {
            InitializeComponent();
            shifts = _shifts;
            dgvShifts.DataSource = shifts;
            dgvShifts.ReadOnly = true;
        }
    }
}
