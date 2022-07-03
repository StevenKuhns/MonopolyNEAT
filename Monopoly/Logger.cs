using System;
using System.IO;
using System.Reflection;

namespace Logging
{
    public class LogStatus
    {
        protected bool skipLogging = false;
    }

    public class Logger: LogStatus
    {
        public bool debug = false;
        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        string fileName;

        public Logger()
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            fileName = $"{di.Parent.FullName}\\Logs\\master.log";
        }

        public Logger(ThreadLogger runner)
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            fileName = $"{di.Parent.FullName}\\Logs\\thread-{runner.Tournament}-{runner.Generation}-{runner.Contestant}-{runner.Group}-{runner.Worker}-{runner.Thread}.log";
        }

        public void Write(string logMessage, bool writeConsole = false)
        {
            try
            {
                if (writeConsole)
                    Console.WriteLine(logMessage);

                if (debug && !skipLogging)
                {
                    using (StreamWriter w = File.AppendText(fileName))
                    {
                        Log (logMessage, w);
                    }
                }
                else if (writeConsole && !skipLogging) // write ONLY console messages to log file
                {
                    using (StreamWriter w = File.AppendText(fileName))
                    {
                        Log (logMessage, w);
                    }
                }
            }
            catch (IOException)
            {
                // skip for now
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex);
            }
        }

        void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine(logMessage);
            }
            catch (IOException)
            {
                // skip for now
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex);
            }
        }

        public void ClearLog()
        {
            try
            {
                File.WriteAllText(fileName, string.Empty);
            }
            catch (IOException)
            {
                // skip for now
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex);
            }
        }
    }

    public class ThreadLogger
    {
        public int Tournament;
        public int Generation;
        public int Contestant;
        public int Group;
        public int Worker;
        public int Thread;
    }
}