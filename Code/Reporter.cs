
using System.IO;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace MDE_2.Code
{
    static class Reporter
    {
        static private string FilePath { get; set; }
        public static void Log(string str)
        {
            using (FileStream fstream = new FileStream(FilePath, FileMode.Append))
            {
                byte[] buffer = Encoding.Default.GetBytes(DateTime.Now.ToString("HH:mm:ss") + "\n" + str);
                fstream.Write(buffer, 0, buffer.Length);
            }
        }
        public static void CreateFile()
        {
            FilePath= "Logs\\log" + DateTime.Now.ToString("ss-mm-HH-dd-MM-yyy") + ".txt";
            using (FileStream fstream = new FileStream(FilePath, FileMode.Create)){}
        }
    }
}
