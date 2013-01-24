using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Project_Opal
{
    class DatabaseConnection
    {

        public static readonly string CONNECTION_STRING = "Data Source=payroll.db3";
        SQLiteConnection con = null;
        SQLiteCommand cmd = null;


        public static DatabaseConnection Open(string conString = "Data Source=payroll.db3")
        {
            DatabaseConnection ret = new DatabaseConnection();

            try
            {
                ret.con = new SQLiteConnection(conString);
                ret.con.Open();

                return ret;
            }
            catch (SQLiteException ex)
            {
                Logger.Write(String.Format("SQLite connection failed: {0}", ex));

                throw ex;
            }
        }

        public void Close()
        {
            if (con != null && con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        //FOR UPDATE/INSERT/DELETE ONLY. Returns the number of affected rows. 
        public int ExecuteUpdate(string sqlString)
        {
            int affectedRows;

            DisposeOldCommand();

            try
            {
                cmd = new SQLiteCommand(sqlString, con);
                affectedRows = cmd.ExecuteNonQuery();

                return affectedRows;
            }
            catch (SQLiteException e)
            {
                Logger.Write(String.Format("SQLite Update/Insert/Delete failed: {0}", e));
                throw e;
            }
        }

        //FOR SELECT STATEMENTS ONLY. Returns the reader which can be foreach'd through 
        public SQLiteDataReader ExecuteSelect(string sqlString)
        {
            SQLiteDataReader resultSet;

            DisposeOldCommand();

            try
            {
                cmd = new SQLiteCommand(sqlString, con);
                resultSet = cmd.ExecuteReader();

                return resultSet;
            }
            catch(SQLiteException e)
            {

                Logger.Write(String.Format("SQLite Select failed: {0} ", e));
                throw e;
            }
        }

        //FOR SELECT STATEMENTS ONLY. Returns first row of first column of result.
        public object ExecuteScalar(string sqlString)
        {
            object result;

            DisposeOldCommand();

            try
            {
                cmd = new SQLiteCommand(sqlString, con);
                result = cmd.ExecuteScalar();

                return result;
            }
            catch (SQLiteException e)
            {

                Logger.Write(String.Format("SQLite Select failed: {0} ", e));
                throw e;
            }
        }

        private void DisposeOldCommand()
        {
            if (cmd != null)
            {
                cmd.Dispose();
            }
        }

        // Destructor is called when GC wants to get rid of object
        ~DatabaseConnection()
        {
            if(con != null)
            {
                con.Dispose();
            }

            if (cmd != null)
            {
                cmd.Dispose();
            }
        }
    }
}
