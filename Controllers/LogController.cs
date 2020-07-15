using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogApi.Models;
using LogApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogApi.Controllers
{

    public class LogController : Controller
    {
        private readonly LoggingDbContext _context;

        public LogController(LoggingDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var logInfos = await _context.LogInfos.ToListAsync();
            return View(logInfos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var logInfo = await _context.LogInfos.FirstOrDefaultAsync(l => l.Id == id);
            return View(logInfo);
        }

        public async Task<IActionResult> Statictis1()
        {   
            var allUserName = await _context.Users.Select(u => u.UserName).OrderBy(l => l).ToListAsync();
            var allLogType = await _context.LogInfos.Select(l => l.Type).Distinct().OrderBy(l => l).ToListAsync();

            var userLogInfos = new List<UserLogInfo>();

            foreach (var user in await _context.Users.Include(u => u.LogInfos).ToListAsync())
            {
                var tmp = new Dictionary<string, int>();
                foreach (var type in allLogType)
                {
                    tmp[type] = 0;
                }

                foreach (var logInfo in user.LogInfos)
                {
                    tmp[logInfo.Type] += 1;
                }
                
                var logTypeCount = new int[allLogType.Count];
                for (int i = 0; i < allLogType.Count; i++)
                {
                    logTypeCount[i] = tmp[allLogType[i]];
                }

                userLogInfos.Add(new UserLogInfo{UserId = user.Id, UserName=user.UserName, LogTypeCount = logTypeCount});
            }
            
            var userLogCount = await _context.Users.Include(u => u.LogInfos).OrderBy(u => u.UserName).Select(u => u.LogInfos.Count()).ToListAsync();

            var typeLogCount = await _context.LogInfos.GroupBy(l => l.Type).OrderBy(g => g.Key).Select(g => g.Count()).ToListAsync();

            var statictis1VM = new Statictis1ViewModel{UserLogInfos = userLogInfos, AllLogType = allLogType, AllUserName = allUserName, UserLogCount = userLogCount, TypeLogCount=typeLogCount};
            return View(statictis1VM);
        }


        
        public async Task<IActionResult> Statictis2()
        {
            var allLogType = await _context.LogInfos.Select(l => l.Type).Distinct().OrderBy(l => l).ToListAsync();
            
            var minMaxLogAccounts = new List<MinMaxLogAccount>();
            foreach (var user in await _context.Users.Include(u => u.LogInfos).Where(u => u.LogInfos.Count() > 0).ToListAsync())
            {
                var maxTime = user.LogInfos.Max(l => l.TimeExcecute);
                var minTime = user.LogInfos.Min(l => l.TimeExcecute);
                var maxId = user.LogInfos.Where(l => l.TimeExcecute == maxTime).Select(u => u.Id).FirstOrDefault();
                var minId = user.LogInfos.Where(l => l.TimeExcecute == minTime).Select(u => u.Id).FirstOrDefault();

                minMaxLogAccounts.Add(new MinMaxLogAccount{UserId = user.Id, UserName = user.UserName, MaxTime = maxTime, MinTime = minTime, MaxLogId = maxId, MinLogId=minId});
            }

            var tmp = new Dictionary<string, MinMaxLogType>();
            var sampleLogInfo = await _context.LogInfos.FirstOrDefaultAsync();
            foreach (var type in allLogType)
            {
                tmp.Add(type, new MinMaxLogType{LogType = type, MinLogId = sampleLogInfo.Id, MaxLogId = sampleLogInfo.Id, MinTime = sampleLogInfo.TimeExcecute, MaxTime = sampleLogInfo.TimeExcecute});
            }

           foreach (var logInfo in await _context.LogInfos.ToListAsync())
           {
               if(tmp[logInfo.Type].MaxTime < logInfo.TimeExcecute)
               {
                   tmp[logInfo.Type].MaxTime = logInfo.TimeExcecute;
                   tmp[logInfo.Type].MaxLogId = logInfo.Id;
               }

               if(tmp[logInfo.Type].MinTime > logInfo.TimeExcecute)
               {
                   tmp[logInfo.Type].MinTime = logInfo.TimeExcecute;
                   tmp[logInfo.Type].MinLogId = logInfo.Id;
               }
           }
           var minMaxLogTypes = tmp.Values.ToList();

            var statictis2VM = new Statictis2ViewModel{MinMaxLogAccounts = minMaxLogAccounts, MinMaxLogTypes = minMaxLogTypes};

            return View(statictis2VM);
        }
    }
}