using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

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
            DatabaseConnection db;
            db = new DatabaseConnection("UserLog.txt");
            log = new Logger(LOG_FILE);

            db.Open();

            string stm = String.Format("SELECT password FROM T_CREDENTIALS WHERE username = '{0}'", username);

            var row = db.ExecuteScalar(stm);

            if (row != null)
            {
                string retrievedPassword = row.ToString();
                string hashedInputPassword = Secure.Hash(password);
                if (retrievedPassword.Equals(hashedInputPassword))
                {
                    // Access granted
                    SQLiteDataReader userInformationReader;

                    stm = String.Format(@"SELECT T_USER.id, T_USER.name, T_USER.address, T_USER.sin, T_USER.bank_account, T_USER.wage
                                        FROM T_USER
                                        INNER JOIN T_CREDENTIALS
                                        ON T_USER.id = T_CREDENTIALS.user_id
                                        WHERE T_CREDENTIALS.username = '{0}'", username);

                    userInformationReader = db.ExecuteSelect(stm);
                    userInformationReader.Read();

                    User ret = new User(userInformationReader.GetInt32(0),
                        userInformationReader.GetString(1),
                        userInformationReader.GetString(2),
                        userInformationReader.GetInt32(3),
                        userInformationReader.GetString(4),
                        userInformationReader.GetDouble(5));

                    db.Close();

                    return ret;
                }
                else
                {
                    // Access denied
                    log.Write(String.Format("{0} failed login request with password: {1}", username.ToUpper(), password));
                    db.Close();
                    return null;
                }
            }
            else
            {
                // User doesn't exist
                log.Write(String.Format("{0} attempted login, but user does not exist", username.ToUpper()));
                db.Close();
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
            DatabaseConnection con = new DatabaseConnection(DatabaseConnection.DATABASE_LOG);
            con.Open();

            SQLiteDataReader latestOpenShift = con.ExecuteSelect(String.Format(@"SELECT id, employee_id, vehicle_number, start_time
                                                                        FROM T_SHIFT 
                                                                        WHERE employee_id = {0}
                                                                        AND end_time IS NULL", id.ToString()));

            if (latestOpenShift.HasRows)
            {
                latestOpenShift.Read();

                Shift shf = new Shift(latestOpenShift.GetInt32(0),
                    latestOpenShift.GetInt32(1),
                    latestOpenShift.GetInt32(2),
                    latestOpenShift.GetDateTime(3));

                con.Close();

                return shf;
            }
            else
            {
                con.Close();

                return null;
            }
        }

        public Shift ClockIn(int vehicleNum)
        {
            DatabaseConnection con = new DatabaseConnection(DatabaseConnection.DATABASE_LOG);
            con.Open();

            string stm = String.Format(@"INSERT INTO T_SHIFT (employee_id, vehicle_number, start_time) VALUES ('{0}', '{1}', date('now'))",
               id.ToString(), vehicleNum.ToString());

        

            con.ExecuteUpdate(stm);

            stm = "SELECT id, MAX(start_date) FROM T_SHIFT WHERE employee_id = '{0}' AND end_time IS NULL";

            var newRowId = con.ExecuteScalar(stm);

            Shift ret = new Shift(Convert.ToInt32(newRowId), id, vehicleNum, DateTime.Now);

            return ret;
        }

        public void ClockOut(Shift s)
        {
            DatabaseConnection con = new DatabaseConnection(DatabaseConnection.DATABASE_LOG);
            con.Open();

            string stm = String.Format(@"UPDATE T_SHIFT SET end_time = date('now') WHERE id = '{0}'", id.ToString());

            con.ExecuteUpdate(stm);

            con.Close();

            s.endTime = DateTime.Now;
        }
    }
}
