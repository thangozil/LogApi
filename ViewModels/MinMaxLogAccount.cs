using System;

namespace LogApi.ViewModels
{
    public class MinMaxLogAccount
    {
        public TimeSpan MaxTime { get; set; }
        public TimeSpan MinTime { get; set; }

        public int MinLogId { get; set; }
        public int MaxLogId { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}