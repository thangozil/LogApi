using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace LogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyControllerBase : ControllerBase
    {
        public string UserId
        {
            get
            {
                return User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }
    }
}