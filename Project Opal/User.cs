using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Project_Opal
{
    public class User
    {
        public int id;
        public string name;
        public string address;
        public int sin;
        public string bankAcctNumber;
        public double wage;

        private static readonly string LOG_FILE = "loginLog.txt";
        private static Logger log;

        public static User Login(string username, string password)
        {
            // Pass password as plaintext
            log = new Logger(LOG_FILE);

            string stm = String.Format("SELECT password FROM T_CREDENTIALS WHERE username = '{0}'", username);

            var row = DatabaseConnection.ExecuteScalar(stm);

            if (row != null)
            {
                string retrievedPassword = row.ToString();
                string hashedInputPassword = Secure.Hash(password);
                if (retrievedPassword.Equals(hashedInputPassword))
                {
                    // Access granted
                    DataTable userInformationTable;

                    stm = String.Format(@"SELECT T_USER.id, T_USER.name, T_USER.address, T_USER.sin, T_USER.bank_account, T_USER.wage
                                        FROM T_USER
                                        INNER JOIN T_CREDENTIALS
                                        ON T_USER.id = T_CREDENTIALS.user_id
                                        WHERE T_CREDENTIALS.username = '{0}'", username);

                    userInformationTable = DatabaseConnection.ExecuteSelect(stm);

                    User ret = new User(Convert.ToInt32(userInformationTable.Rows[0][0]),
                        userInformationTable.Rows[0][1].ToString(),
                        userInformationTable.Rows[0][2].ToString(),
                        Convert.ToInt32(userInformationTable.Rows[0][3]),
                        userInformationTable.Rows[0][4].ToString(),
                        Convert.ToDouble(userInformationTable.Rows[0][5]));

                    return ret;
                }
                else
                {
                    // Access denied
                    log.Write(String.Format("{0} failed login request with password: {1}", username.ToUpper(), password));
                    return null;
                }
            }
            else
            {
                // User doesn't exist
                log.Write(String.Format("{0} attempted login, but user does not exist", username.ToUpper()));
                return null;
            }
        }

        private User(int i_id, string i_name, string i_address, int i_sin, string i_bankAcctNumber, double i_wage)
        {
            id = i_id;
            name = i_name;
            address = i_address;
            sin = i_sin;
            bankAcctNumber = i_bankAcctNumber;
            wage = i_wage;

            log.Write(String.Format("{0} has logged in successfully.", name.ToUpper()));
        }

        public Shift GetOpenShift()
        {
            DataTable latestOpenShiftTable = DatabaseConnection.ExecuteSelect(String.Format(@"SELECT id, employee_id, vehicle_number, start_time
                                                                        FROM T_SHIFT 
                                                                        WHERE employee_id = {0}
                                                                        AND end_time IS NULL", id.ToString()));

            if (latestOpenShiftTable.Rows.Count > 0)
            {
                Shift shf = new Shift(Convert.ToInt32(latestOpenShiftTable.Rows[0][0]),
                    Convert.ToInt32(latestOpenShiftTable.Rows[0][1]),
                    Convert.ToInt32(latestOpenShiftTable.Rows[0][2]),
                    Convert.ToDateTime(latestOpenShiftTable.Rows[0][3]));

                return shf;
            }
            else
            {
                return null;
            }
        }

        public Shift ClockIn(int vehicleNum)
        {
            DatabaseConnection con = new DatabaseConnection(DatabaseConnection.DATABASE_LOG);
            con.Open();

            string stm = String.Format(@"INSERT INTO T_SHIFT (employee_id, vehicle_number, start_time) VALUES ('{0}', '{1}', date('now'))",
               id.ToString(), vehicleNum.ToString());

        

            DatabaseConnection.ExecuteUpdate(stm);

            stm = String.Format("SELECT id FROM T_SHIFT WHERE employee_id = '{0}' AND end_time IS NULL", id);

            var newRowId = DatabaseConnection.ExecuteScalar(stm);

            Shift ret = new Shift(Convert.ToInt32(newRowId), id, vehicleNum, DateTime.Now);

            return ret;
        }

        public void ClockOut(Shift s)
        {
            string stm = String.Format(@"UPDATE T_SHIFT SET end_time = date('now') WHERE id = '{0}'", s.id);

            DatabaseConnection.ExecuteUpdate(stm);

            s.endTime = DateTime.Now;
        }
        
        public Shift[] PreviousShifts(User user)
        {
            DateTime NowDate = DateTime.Now;

            DataTable PreviousShiftTable = DatabaseConnection.ExecuteSelect(String.Format(@"SELECT id, employee_id, vehicle_number, start_time, end_time
                                                                        FROM T_SHIFT 
                                                                        WHERE employee_id = {0}", user.id.ToString()));

            Shift[] shiftArray = new Shift[PreviousShiftTable.Rows.Count];
            
            //Need to add exception for when the user has an unfinished shift
            for (int i = 0; i < PreviousShiftTable.Rows.Count; i++)
            {
                    Shift shf = new Shift(Convert.ToInt32(PreviousShiftTable.Rows[i][0]),
                      Convert.ToInt32(PreviousShiftTable.Rows[i][1]),
                      Convert.ToInt32(PreviousShiftTable.Rows[i][2]),
                      Convert.ToDateTime(PreviousShiftTable.Rows[i][3]),
                      Convert.ToDateTime(PreviousShiftTable.Rows[i][4]));
                    shiftArray[i] = shf;
            }
            return shiftArray;
        }
    }
}
