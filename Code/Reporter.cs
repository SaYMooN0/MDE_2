
using System.IO;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace MDE_2.Code
{
    static class Reporter
    {
        public static async void Log(string str)
        {
            byte[] buffer = Encoding.Default.GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n" + str);
            using (FileStream fstream = new FileStream("log.txt", FileMode.OpenOrCreate))
            {
                await fstream.WriteAsync(buffer, 0, buffer.Length);
            }
        }
    }
}
