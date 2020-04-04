using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data
{
    public class WebApiContext : IdentityDbContext<WebApiUser>
    {
        public WebApiContext(DbContextOptions options) : base(options)
        {

        }
    }
}
