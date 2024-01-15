using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Lawtech.Utils
{
    public class Log
    {
        public static void GravarLog(bool isErrorLog, string msg)
        {
            string path;
            string directory = "Logs/" + DateTime.Now.ToString("dd-MM-yyyy");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!isErrorLog)
            {
                path = DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            }
            else
            {
                path = DateTime.Now.ToString("dd-MM-yyyy") + "-Error.txt";
            }

            path = directory + "/" + path;

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(DateTime.Now.ToString() + " - " + msg);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(DateTime.Now.ToString() + " - " + msg);
                }
            }
        }
    }
}
