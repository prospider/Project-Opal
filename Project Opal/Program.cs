using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Project_Opal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        // DEBUG MODE
        public static readonly bool DEBUG_MODE = true;

        private static Logger log;
        public static User currentUser;

        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            initLogger();
            CheckDatabase();
            if (DEBUG_MODE) { SeedDebugDatabase(); }

            Login_Form loginForm = new Login_Form();

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                currentUser = loginForm.currentUser;
                Application.Run(new MainMenu_Form(currentUser));
            }
            else
            {
                Application.Exit();
            }
        }

        static void initLogger()
        {
            log = new Logger("ProgramLog.txt");
        }

        static void CheckDatabase()
        {
            DatabaseConnection DatabaseConnection;

            try
            {
                string stm = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'T_SHIFT'";
                log.Write(String.Format("Executed Query: {0}", stm));
                var rows = DatabaseConnection.ExecuteScalar(stm);

                /// <warning>
                /// If you change these tables structures, BE SURE TO DELETE payroll.DatabaseConnection3 IN THE DEBUG FOLDER.
                /// If you don't, you're gonna have a bad time.
                /// </warning>

                if (rows == null) // No rows found, database is empty
                {
                    log.Write("Uh oh, we didnt find any info in the database! Recreating Database.");
                    stm = @"CREATE TABLE T_SHIFT
                            (
                                id INTEGER PRIMARY KEY ASC AUTOINCREMENT,
                                employee_id INTEGER,
                                vehicle_number INTEGER NOT NULL,
                                start_time DATE NOT NULL,
                                end_time DATE,
                                FOREIGN KEY(id) REFERENCES T_USER(id)
                            );";

                    rows = DatabaseConnection.ExecuteUpdate(stm);

                    stm = @"CREATE TABLE T_USER
                            (
                                id INTEGER PRIMARY KEY ASC AUTOINCREMENT,
                                name TEXT NOT NULL,
                                address TEXT NOT NULL,
                                sin INTEGER UNIQUE NOT NULL,
                                bank_account TEXT NOT NULL,
                                wage REAL NOT NULL
                            );";

                    rows = DatabaseConnection.ExecuteUpdate(stm);

                    stm = @"CREATE TABLE T_CREDENTIALS
                            (
                                id INTEGER PRIMARY KEY ASC AUTOINCREMENT,
                                user_id INTEGER UNIQUE NOT NULL,
                                username TEXT UNIQUE NOT NULL,
                                password TEXT NOT NULL,
                                FOREIGN KEY(user_id) REFERENCES T_USER(id)
                            );";

                    rows = DatabaseConnection.ExecuteUpdate(stm);
                }
                //TODO: Double check that tables were created
            }
            catch (SQLiteException ex)
            {
                log.Write(String.Format("Problem communicating with the database, or file already exists. Error info: {0}",ex.ToString()),Logger.CRITICAL);

                System.Windows.Forms.MessageBox.Show("Database setup failed. SQL error detected & logged.");
                Environment.Exit(-1);
            }
        }

        static void SeedDebugDatabase()
        {
            try
            {
                log.Write("Seeding Data into new database");

                string stm = "SELECT * FROM 'T_USER'";
                log.Write(string.Format("Executing SQL: {0}", stm));
                var rows = DatabaseConnection.ExecuteScalar(stm);

                if (rows == null)
                {
                    /// <warning>
                    /// If you change these seed values, BE SURE TO DELETE payroll.DatabaseConnection3 IN THE DEBUG FOLDER.
                    /// If you don't, you're gonna have a bad time.
                    /// </warning>
                    /// 
                    stm = @"INSERT INTO 'T_USER' ('name', 'address', 'sin', 'bank_account', 'wage')
                                    SELECT 'Bob Smith' AS 'name', '123 Fake St.' AS 'address', '123456789' AS 'sin', '1-01' AS 'bank_account',
                                        '12.00' AS 'wage'
                            UNION   SELECT 'Mary Jones' AS 'name', '345 Carnival Rd.' AS 'address', '987654321' AS 'sin', '1-02' AS 'bank_account',
                                        '14.00' AS 'wage'
                            UNION   SELECT 'Tom Hardy' AS 'name', '909 Batman Pl.' AS 'address', '555666777' AS 'sin', '1-03' AS 'bank_account',
                                        '5667.00' AS 'wage'
                            ";

                    rows = DatabaseConnection.ExecuteUpdate(stm);

                    stm = String.Format(@"INSERT INTO 'T_CREDENTIALS' ('user_id', 'username', 'password')
                                    SELECT '1' AS 'user_id', 'bob' AS 'username', '{0}' AS 'password'
                            UNION   SELECT '2' AS 'user_id', 'mary' AS 'username', '{1}' AS 'password'
                            UNION   SELECT '3' AS 'user_id', 'tom' AS 'username', '{2}' AS 'password'
                            ", Secure.Hash("opal"), Secure.Hash("ruby"), Secure.Hash("topaz"));

                    rows = DatabaseConnection.ExecuteUpdate(stm);

                    stm = @"INSERT INTO 'T_SHIFT' ('employee_id', 'vehicle_number', 'start_time', 'end_time')
                                    SELECT '1' AS 'employee_id', '10' AS 'vehicle_number', '2013-01-01 09:00' AS 'start_time', '2013-01-01 17:00' as 'end_time'
                            UNION   SELECT '3' AS 'employee_id', '13' AS 'vehicle_number', '2013-01-01 09:00' AS 'start_time', '2013-01-01 17:00' as 'end_time'";
                    //TODO: Verify insert succeeded

                    rows = DatabaseConnection.ExecuteUpdate(stm);

                    stm = @"INSERT INTO 'T_SHIFT' ('employee_id', 'vehicle_number', 'start_time')
                                    SELECT '2' AS 'employee_id', '11' AS 'vehicle_number', '2013-01-01 09:00' AS 'start_time'
                            UNION   SELECT '3' AS 'employee_id', '13' AS 'vehicle_number', '2013-01-02 09:00' AS 'start_time'";

                    rows = DatabaseConnection.ExecuteUpdate(stm);
                }
            }
            catch (SQLiteException ex)
            {
                log.Write(ex.ToString());

                System.Windows.Forms.MessageBox.Show("Database seeding failed. SQL error detected & logged.");
                Environment.Exit(-1);
            }
        }
    }
}
