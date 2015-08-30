using System;

namespace NLogViewer.Models
{
    public class Log
    {
        public long Id { get; set; }
        public DateTime DateUtc { get; set; }
        public int ThreadId { get; set; }
        public string Level { get; set; }
        public string MachineName { get; set; }
        public string SubwEnvironment { get; set; }
        public string Logger { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }


        public string ShortenedLogger
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Logger))
                {
                    return Logger;
                }

                if (!Logger.Contains("."))
                {
                    return Logger;
                }

                var lastDotPos = Logger.LastIndexOf(".");
                return string.Concat("...", Logger.Substring(lastDotPos + 1, (Logger.Length - lastDotPos) - 1));
            }
        }
    }
}