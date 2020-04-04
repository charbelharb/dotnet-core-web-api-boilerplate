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
            string result = "";
            ClaimsPrincipal _curUser = HttpContext.User;
            if (_curUser != null)
            {
                Claim _curUserId = _curUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (_curUserId != null && !string.IsNullOrEmpty(_curUserId.Value))
                {
                    result = _curUserId.Value;
                }
            }
            return result;
        }

        protected string CurrentUser => GetCurrentUser();
    }
}
