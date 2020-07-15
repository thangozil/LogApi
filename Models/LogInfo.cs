using System;

namespace LogApi.Models
{
    public class LogInfo
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public TimeSpan TimeExcecute { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}