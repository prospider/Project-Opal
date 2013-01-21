﻿using System;
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

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CheckDatabase();
            if (DEBUG_MODE) { SeedDebugDatabase(); }
            Application.Run(new Login_Form());
        }

        static void CheckDatabase()
        {
            SQLiteConnection con = null;
            SQLiteCommand cmd = null;

            try
            {
                con = new SQLiteConnection(Database.CONNECTION_STRING);
                con.Open();

                string stm = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'T_SHIFT'";

                cmd = new SQLiteCommand(stm, con);

                var rows = cmd.ExecuteScalar();

                /// <warning>
                /// If you change these tables structures, BE SURE TO DELETE payroll.db3 IN THE DEBUG FOLDER.
                /// If you don't, you're gonna have a bad time.
                /// </warning>

                if (rows == null) // No rows found, database is empty
                {
                    stm = @"CREATE TABLE T_SHIFT
                            (
                                id INTEGER PRIMARY KEY ASC AUTOINCREMENT,
                                employee_id INTEGER,
                                vehicle_number INTEGER NOT NULL,
                                start_time DATE NOT NULL,
                                end_time DATE
                            );";

                    cmd.Dispose();
                    cmd = new SQLiteCommand(stm, con);
                    rows = cmd.ExecuteNonQuery();

                    stm = @"CREATE TABLE T_USER
                            (
                                id INTEGER PRIMARY KEY ASC AUTOINCREMENT,
                                employee_id INTEGER UNIQUE NOT NULL,
                                username TEXT UNIQUE NOT NULL,
                                password TEXT NOT NULL
                            );";    // TODO: password will have to be hashed and stored

                    cmd.Dispose();
                    cmd = new SQLiteCommand(stm, con);
                    rows = cmd.ExecuteNonQuery();
                }

                //TODO: Double check that tables were created
            }
            catch (SQLiteException ex)
            {
                Logger.Write(ex.ToString());
            }
            finally
            {
                Database.CloseAndDispose(con, cmd);
            }
        }

        static void SeedDebugDatabase()
        {
            SQLiteConnection con = null;
            SQLiteCommand cmd = null;

            try
            {
                con = new SQLiteConnection(Database.CONNECTION_STRING);
                con.Open();

                string stm = "SELECT * FROM 'T_USER'";

                cmd = new SQLiteCommand(stm, con);
                var rows = cmd.ExecuteScalar();

                if (rows == null)
                {
                    /// <warning>
                    /// If you change these seed values, BE SURE TO DELETE payroll.db3 IN THE DEBUG FOLDER.
                    /// If you don't, you're gonna have a bad time.
                    /// </warning>

                    stm = @"INSERT INTO 'T_USER' ('employee_id', 'username', 'password')
                                    SELECT '1' AS 'employee_id', 'bob' AS 'username', 'opal' AS 'password'
                            UNION   SELECT '2' AS 'employee_id', 'mary' AS 'username', 'ruby' AS 'password'
                            UNION   SELECT '3' AS 'employee_id', 'tom' AS 'username', 'topaz' AS 'password'
                            ";

                    cmd.Dispose();
                    cmd = new SQLiteCommand(stm, con);
                    rows = cmd.ExecuteNonQuery();

                    //TODO: Verify insert succeeded
                }
            }
            catch (SQLiteException ex)
            {
                Logger.Write(ex.ToString());
            }
            finally
            {
                Database.CloseAndDispose(con, cmd);
            }
        }
    }
}
