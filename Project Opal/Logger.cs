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
        static string[] severityList = { "INFO", "ERROR", "CRITICAL" };
        StreamWriter file;
        string logFileString;
        static StreamWriter runFile;
        static string runLogString = "runLog.txt";
        static bool runInit = true;
        public Logger(string logFileString)
        {
            this.logFileString = logFileString;
            StartRun();
        }



        ~Logger()
        {
            try
            {
                ;//file.Close();
            }
            catch (Exception e)
            {
                ;
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
                
            }

        }

        public void StartRun()
        {
            file = new StreamWriter(this.logFileString, true);
            file.WriteLine("------------------NEW RUN---------------------");
            file.Close();

            if (runInit)
            {
                runFile = new StreamWriter(runLogString, true);
                runFile.WriteLine("------------------NEW RUN---------------------");
                runFile.Close();
                runInit = false;
            }
        }
    }
}
