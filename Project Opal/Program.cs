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
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void CheckDatabase()
        {
            SQLiteConnection con = null;
            SQLiteCommand cmd = null;

            try
            {
                con = new SQLiteConnection(Database.CONNECTION_STRING);
                con.Open();

                string stm = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'T_PERSON'";

                cmd = new SQLiteCommand(stm, con);

                //TODO: If table exists, leave alone, if not, create database tables and continue

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
