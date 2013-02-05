using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Opal
{
    public class Shift
    {
        public int id;
        public int employeeId;
        public int vehicleNumber;
        public DateTime startTime;
        public DateTime endTime;

        public Shift(int i_id, int i_employeeId, int i_vehicleNumber, DateTime i_startTime)
        {
            id = i_id;
            employeeId = i_employeeId;
            vehicleNumber = i_vehicleNumber;
            startTime = i_startTime;
        }

        public Shift(int i_id, int i_employeeId, int i_vehicleNumber, DateTime i_startTime, DateTime i_endTime)
        {
            id = i_id;
            employeeId = i_employeeId;
            vehicleNumber = i_vehicleNumber;
            startTime = i_startTime;
            endTime = i_endTime;
        }
    }
}
