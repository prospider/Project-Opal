using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_Opal
{
    class Logger
    {
        public static void Write(string line)
        {
            StreamWriter file = new StreamWriter("log.txt", true);

            file.WriteLine(String.Format("{0}:\t{1}", DateTime.Now.ToString(), line));

            file.Close();
        }
    }
}
