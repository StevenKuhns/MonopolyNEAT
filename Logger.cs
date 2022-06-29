using System;
using System.IO;
using System.Reflection;

namespace Monopoly
{
    static class LogWriter
    {
        static DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());

        public static void Write(string logMessage, int runner, bool writeConsole = false)
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            string fileName = $"{di.Parent.FullName}\\log-{runner}.txt";
            try
            {
                using (StreamWriter w = File.AppendText(fileName))
                {
                    Log (logMessage, w);
                }

                if (writeConsole)
                    Console.WriteLine(logMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex);
            }
        }

        public static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine("  :{0}", logMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex);
            }
        }

        public static void ClearLog(int runner)
        {
            string fileName = $"{di.Parent.FullName}\\log-{runner}.txt";
            File.WriteAllText(fileName, string.Empty);
        }
    }
}
