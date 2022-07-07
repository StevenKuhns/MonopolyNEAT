using System;
using System.IO;
using System.Reflection;

namespace Logging
{
    public class Logger
    {
        public bool debug = false;
        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        public string fileName;
        public bool skipLogging = false;

        public Logger()
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            fileName = $"{di.FullName}\\Logs\\master.log";
            if (Directory.Exists($"{di.FullName}\\Logs") == false)
                Directory.CreateDirectory($"{di.FullName}\\Logs");
        }

        public Logger(ThreadLogger runner)
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            fileName = $"{di.FullName}\\Logs\\{ThreadFileName(runner)}.log";
            if (Directory.Exists($"{di.FullName}\\Logs") == false)
                Directory.CreateDirectory($"{di.FullName}\\Logs");
        }

        public void Write(string logMessage, bool writeConsole = false)
        {
            try
            {
                if (writeConsole)
                    Console.WriteLine(logMessage);
                
                if (skipLogging)
                    return;

                if (debug)
                {
                    using (StreamWriter w = File.AppendText(fileName))
                    {
                        Log (logMessage, w);
                    }
                }
                else if (writeConsole) // write ONLY console messages to log file
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

        public void DeleteLog()
        {
            try
            {
                File.Delete(fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex);
            }
        }

        string ThreadFileName(ThreadLogger runner)
        {
            return $"thread-{runner.Tournament}-{runner.Generation}-{runner.Contestant}-{runner.Group}-{runner.Worker}-{runner.Thread}";
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