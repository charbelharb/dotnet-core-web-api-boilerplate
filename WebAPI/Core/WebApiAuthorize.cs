using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;

namespace WebAPI.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class WebApiAuthorize : AuthorizeAttribute
    {
        public WebApiAuthorize()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
