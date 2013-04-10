using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_Opal
{
    public class Logger
    {
        // Other methods can write using these severity levels rather than array indices
        public static readonly int INFO = 0;
        public static readonly int ERROR = 1;
        public static readonly int CRITICAL = 2;

        static string[] severityList = { "INFO", "ERROR", "CRITICAL" };
        StreamWriter file;
        string logFileString;
        static StreamWriter runFile;
        static string runLogString = "runLog.txt";
        static private bool initStatus = true;

        public Logger(string logFileString)
        {
            this.logFileString = logFileString;
            if (initStatus)
            {
                runInit();
                initStatus = false;
            }
          
        }

        public void Write(string line, int severity = 0)
        {
            try
            {
                file = new StreamWriter(this.logFileString, true);
                runFile = new StreamWriter(runLogString, true);
                runFile.WriteLine(String.Format("{0}:\t{1} -> {2}", DateTime.Now.ToString(), severityList[severity], line));
                file.WriteLine(String.Format("{0}:\t{1} -> {2}", DateTime.Now.ToString(), severityList[severity], line));
                file.Close();
                runFile.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Something serious happened to Logger. Error: {0}", e.ToString()));
            }

        }

        private void runInit()
        {
            runFile = new StreamWriter(runLogString, true);
            runFile.WriteLine("\n----------------NEW RUN------------NEW RUN -----------NEW RUN------------NEW RUN----------------NEW RUN------------------NEW RUN--------------------NEW RUN--------------\n");
            runFile.Close();
        }
    }
}
