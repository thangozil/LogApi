using System.Collections.Generic;

namespace LogApi.ViewModels
{
    public class Statictis1ViewModel
    {
        public IEnumerable<UserLogInfo> UserLogInfos { get; set; }
        public IEnumerable<string> AllLogType { get; set; }

        public IEnumerable<string> AllUserName { get; set; }

        public IEnumerable<int> UserLogCount { get; set; }

        public IEnumerable<int> TypeLogCount { get; set; }
    }
}