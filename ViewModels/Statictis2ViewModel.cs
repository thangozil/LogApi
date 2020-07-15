using System.Collections.Generic;

namespace LogApi.ViewModels
{
    public class Statictis2ViewModel
    {
        public IEnumerable<MinMaxLogAccount> MinMaxLogAccounts { get; set; }

        public IEnumerable<MinMaxLogType> MinMaxLogTypes { get; set; }
    }
}