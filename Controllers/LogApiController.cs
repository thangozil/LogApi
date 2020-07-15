using System.Threading.Tasks;
using LogApi.Models;
using LogApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogApi.Controllers
{
    [Authorize]
    public class LogApiController : MyControllerBase
    {
        private readonly LoggingDbContext _context;

        public LogApiController(LoggingDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(LogInfo logInfo)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new Error("Nonvalid data"));
            }
            
            logInfo.UserId = UserId;

            _context.LogInfos.Add(logInfo);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}