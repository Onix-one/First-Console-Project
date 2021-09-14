using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Pilot_Project
{
    public class Logger : Exception
    {
        private readonly string _incomingMessage;
        public string Dedug => "Debug";
        public string Info => "Info";
        public string Error => "Error";
        public Logger() { }
        public Logger(string value)
            : base(value)
        {
            _incomingMessage = value;
        }
        public void LogWrite(string logType)
        {
            var methodInfo = new StackFrame(1).GetMethod();
            Thread thread = Thread.CurrentThread;
            string logMessage = $"Thread: Priority: {thread.Priority}, Id: {thread.ManagedThreadId}, " +
                                $"Background: {{thread.IsBackground}}, Pool: {thread.IsThreadPoolThread}, " +
                                $"State: {thread.ThreadState}. \nLocation: Namespace: {methodInfo.ReflectedType?.FullName}. " +
                                $"Method: {methodInfo.Name}. \nMessage: {_incomingMessage}";
            using (TextWriter writer = File.AppendText(GetFilePathAndCheckFileSize()))
            {
                writer.Write($"\r\n[{logType}]: ");
                writer.WriteLine("(UTC) {0} {1}", DateTime.UtcNow.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                writer.WriteLine($"{logMessage}");
                writer.WriteLine(new string('.', 40));
            }
        }
        private string GetFilePathAndCheckFileSize()
        {
            int counter = 1;
            while (true)
            {
                string fileDirectory = Directory.CreateDirectory("Logs") + @".\\" +
                                       "log " + DateTime.UtcNow.ToString("yyyy-MM-dd_");
                string fileFormat = ".txt";
                string filePath = fileDirectory + counter + fileFormat;
                FileInfo logFile = new FileInfo(filePath);
                if (logFile.Exists)
                {
                    if (logFile.Length > 30_000) { counter++; continue; }
                }
                return filePath;
            }
        }
    }
}