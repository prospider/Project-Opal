using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Project_Opal
{
    class User
    {
        public string name;
        public string address;
        public int sin;
        public string bankAcctNumber;
        public double wage;
        private static Logger log;

        public static User Login(string username, string password)
        {
            // Pass password as plaintext
            DatabaseConnection db;
            db = new DatabaseConnection("UserLog.txt");
            log = new Logger("UserLog.txt");

            db.Open();

            string stm = String.Format("SELECT password FROM T_CREDENTIALS WHERE username = '{0}'", username);

            var row = db.ExecuteScalar(stm);

            if (row != null)
            {
                string retrievedPassword = row.ToString();
                string hashedInputPassword = Secure.Hash(password);
                log.Write(string.Format("Testing inputted hash: {0}", hashedInputPassword.ToString() ));
                if (retrievedPassword.Equals(hashedInputPassword))
                {
                    // Access granted
                    log.Write("Access granted!");
                    SQLiteDataReader userInformationReader;

                    stm = String.Format(@"SELECT T_USER.name, T_USER.address, T_USER.sin, T_USER.bank_account, T_USER.wage, T_CREDENTIALS.username 
                                        FROM T_USER
                                        INNER JOIN T_CREDENTIALS
                                        ON T_CREDENTIALS.user_id = T_USER.id
                                        WHERE T_CREDENTIALS.username = '{0}'", username);

                    userInformationReader = db.ExecuteSelect(stm);
                    bool test = userInformationReader.HasRows;

                    User ret = new User(userInformationReader.GetString(0),
                        userInformationReader.GetString(1),
                        userInformationReader.GetInt32(2),
                        userInformationReader.GetString(3),
                        userInformationReader.GetDouble(4));

                    db.Close();

                    return ret;
                }
                else
                {
                    // Access denied
                    log.Write("Access Denied!");
                    db.Close();
                    return null;
                }
            }
            else
            {
                // User doesn't exist
                log.Write("User doesnt exist!");
                db.Close();
                return null;
            }
        }

        public User(string i_name, string i_address, int i_sin, string i_bankAcctNumber, double i_wage)
        {
            name = i_name;
            address = i_address;
            sin = i_sin;
            bankAcctNumber = i_bankAcctNumber;
            wage = i_wage;
        }
    }
}
