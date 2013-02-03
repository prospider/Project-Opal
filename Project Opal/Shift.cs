using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Opal
{
    class Shift
    {
        public int id;
        public int employeeId;
        public int vehicleNumber;
        public DateTime startDate;
        public DateTime endDate;

        public Shift(int i_id, int i_employeeId, int i_vehicleNumber, DateTime i_startDate)
        {
            id = i_id;
            employeeId = i_employeeId;
            vehicleNumber = i_vehicleNumber;
            startDate = i_startDate;
        }

        public Shift(int i_id, int i_employeeId, int i_vehicleNumber, DateTime i_startDate, DateTime i_endDate)
        {
            id = i_id;
            employeeId = i_employeeId;
            vehicleNumber = i_vehicleNumber;
            startDate = i_startDate;
            endDate = i_endDate;
        }

        public static Shift ClockIn(int userId, int vehicleNum)
        {
            DatabaseConnection con = new DatabaseConnection(DatabaseConnection.DATABASE_LOG);
            con.Open();

            string stm = String.Format(@"INSERT employee_id, vehicle_number, start_time INTO T_SHIFT VALUES ('{0}', '{1}', date('now'))",
                userId.ToString(), vehicleNum.ToString());

            con.ExecuteUpdate(stm);

            stm = "SELECT id, MAX(start_date) FROM T_SHIFT WHERE employee_id = '{0}' AND end_time IS NULL";

            var newRowId = con.ExecuteScalar(stm);

            Shift ret = new Shift(Convert.ToInt32(newRowId), userId, vehicleNum, DateTime.Now);

            return ret;
        }

        public void ClockOut()
        {
            DatabaseConnection con = new DatabaseConnection(DatabaseConnection.DATABASE_LOG);
            con.Open();

            string stm = String.Format(@"UPDATE T_SHIFT SET end_time = date('now') WHERE id = {0}'", id.ToString());

            con.ExecuteUpdate(stm);

            con.Close();

            endDate = DateTime.Now;
        }
    }
}
