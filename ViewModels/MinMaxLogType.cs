using System;

namespace LogApi.ViewModels
{
    public class MinMaxLogType
    {
        public TimeSpan MaxTime { get; set; }
        public TimeSpan MinTime { get; set; }

        public int MinLogId { get; set; }
        public int MaxLogId { get; set; }

        public string LogType { get; set; }
    }
}