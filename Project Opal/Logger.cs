using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Opal
{
    class Logger
    {
        public static void Write(string line)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("%APPDATA%\\Project Opal\\log.txt", true);

            file.WriteLine(String.Format("{0}:\t{1}", DateTime.Now.ToString(), line);

            file.Close();
        }
    }
}
