using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Project_Opal
{
    class Database
    {
        public static readonly string CONNECTION_STRING = "Data Source=payroll.db3";

        public static void CloseAndDispose(SQLiteConnection con, SQLiteCommand cmd)
        {
            if(con != null)
            {
                try
                {
                    con.Close();
                }
                catch (SQLiteException ex)
                {
                    Logger.Write(ex.ToString());
                }
                finally
                {
                    con.Dispose();
                }
            }

            if (cmd != null)
            {
                cmd.Dispose();
            }
        }
    }
}
