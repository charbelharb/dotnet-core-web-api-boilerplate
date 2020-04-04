using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data
{
    public class WebApiContext : IdentityDbContext<WebApiUser>
    {
        public WebApiContext(DbContextOptions options) : base(options)
        {

        }
    }
}
