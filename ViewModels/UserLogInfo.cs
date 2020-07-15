using System.Collections.Generic;

namespace LogApi.ViewModels
{
    public class UserLogInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        
        public int[] LogTypeCount{get;set;}

    }
}