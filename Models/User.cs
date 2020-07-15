using System;
using System.Collections.Generic;

namespace LogApi.Models
{
    public class User
    {
        public User()
        {
            Date = DateTime.Now;
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<LogInfo> LogInfos {get;set;}

    }
}