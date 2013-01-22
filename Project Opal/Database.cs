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
        SQLiteConnection con = null;
        SQLiteCommand cmd = null;


        void Database(string conString = "Data Source=payroll.db3")
        {
            try
            {
                con = new SQLiteConnection(conString);
                con.Open();
            }
            catch (SQLiteException ex)
            {
                Logger.Write(ex.ToString());
            }
        }

        //FOR UPDATE/INSERT/DELETE ONLY. Returns the number of affected rows. 
        public int executeUpdate(string sqlString)
        {
            int affectedRows;
            try
            {
                cmd = new SQLiteCommand(sqlString, this.con);
                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (SQLiteException e)
            {
                Logger.Write("SQLITE ERROR: " + e);
                return 0;
            }

            return affectedRows;
            
        }

        //FOR SELECT STATEMENTS ONLY. Returns the reader which can be foreach'd through 
        public SQLiteDataReader executeSelect(string sqlString)
        {
            SQLiteDataReader resultSet;
            try
            {
                cmd = new SQLiteCommand(sqlString, this.con);
                resultSet = cmd.ExecuteReader();
            }
            catch(SQLiteException e)
            {

                Logger.Write("Failed to execute Select Statement: " + e);
                return null;
            }

            return resultSet;
        }


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
