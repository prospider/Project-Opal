using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Project_Opal
{
    public class DatabaseConnection
    {
        public static readonly string CONNECTION_STRING = "Data Source=payroll.db3";
        public static readonly string DATABASE_LOG = "Database_Log.txt";

        SQLiteConnection con = null;
        SQLiteCommand cmd = null;
        private Logger log;
        private string conString;

        public DatabaseConnection(string logFile, string conString = "Data Source=payroll.db3")
        {
            this.conString = conString;
            this.log = new Logger(logFile); //this allows the database module to write to the parent's logfile so we can see which parts of the program are running which SQL.
        }

        public void Open()
        {
            try
            {
                this.con = new SQLiteConnection(this.conString);
                this.con.Open();
                log.Write(String.Format("Connection opened to: {0}", conString));
            }
            catch (SQLiteException ex)
            {
                log.Write(String.Format("SQLite connection failed: {0}", ex),1);

                throw ex;
            }
        }

        public void Close()
        {
            if (con != null && con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
                log.Write(String.Format("Closed connection to Database {0}", this.conString));
            }
        }

        //FOR UPDATE/INSERT/DELETE ONLY. Returns the number of affected rows. 
        static public int ExecuteUpdate(string sqlString)
        {
            using(SQLiteConnection con = new SQLiteConnection(CONNECTION_STRING))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(sqlString, con))
                {
                    int affectedRows;

                    try
                    {
                        affectedRows = cmd.ExecuteNonQuery();
                        //log.Write(String.Format("Executed Update: {0}\nRows Affected: {1}", sqlString, affectedRows));
                        return affectedRows;
                    }
                    catch (SQLiteException e)
                    {
                        //log.Write(String.Format("SQLite Update/Insert/Delete failed: {0}\nFailure caused by: {1}", e, sqlString), 1);
                        throw e;
                    }
                    finally
                    {
                        cmd.Dispose();
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }

        //FOR SELECT STATEMENTS ONLY. Returns the reader which can be foreach'd through 
        static public DataTable ExecuteSelect(string sqlString)
        {
            using (SQLiteConnection con = new SQLiteConnection(CONNECTION_STRING))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(sqlString, con))
                {
                    SQLiteDataReader resultSet;
                    DataTable dt;

                    try
                    {
                        resultSet = cmd.ExecuteReader();
                        dt = new DataTable();
                        dt.Load(resultSet);
                        //log.Write(String.Format("Select Executed Successfully: {0}", sqlString));
                        return dt;
                    }
                    catch (SQLiteException e)
                    {
                        //log.Write(String.Format("SQLite Select failed: {0}\nFailure caused by: {1} ", e, sqlString), 1);
                        throw e;
                    }
                    finally
                    {
                        cmd.Dispose();
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }

        //FOR SELECT STATEMENTS ONLY. Returns first row of first column of result.
        static public object ExecuteScalar(string sqlString)
        {
            using (SQLiteConnection con = new SQLiteConnection(CONNECTION_STRING))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(sqlString, con))
                {
                    object result;

                    try
                    {
                        result = cmd.ExecuteScalar();
                        //log.Write(String.Format("Scalar Executed Successfully: {0}", sqlString));
                        return result;
                    }
                    catch (SQLiteException e)
                    {
                        //log.Write(String.Format("SQLite Select failed: {0} ", e), 1);
                        throw e;
                    }
                    catch (InvalidOperationException e)
                    {
                        System.Windows.Forms.MessageBox.Show(String.Format("Your SQL statement was incorrect. SQL: {0}",
                            sqlString));
                        throw e;
                    }
                    finally
                    {
                        cmd.Dispose();
                        con.Close();
                        con.Dispose();
                    }
                }
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
            try
            {

                con.Dispose();

                if (con != null)
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                        log.Write("Connection to Database closed.");
                    }

                    con.Dispose();
                }
            }
            catch (Exception e)
            {
                log.Write(String.Format("Could not close and dispose DatabaseConnection Connection: {0}" , e.ToString()), 1);
            }

            if (cmd != null)
            {
                log.Write(string.Format("command disposed of: {0}", cmd));
                cmd.Dispose();
            }
        }
    }
}
