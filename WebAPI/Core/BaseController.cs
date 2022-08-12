using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Core
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private string GetCurrentUser()
        {
            var result = "";
            var curUser = HttpContext.User;
            var curUserId = curUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (curUserId != null && !string.IsNullOrEmpty(curUserId.Value))
            {
                result = curUserId.Value;
            }
            return result;
        }

        protected string CurrentUser => GetCurrentUser();
    }
}
