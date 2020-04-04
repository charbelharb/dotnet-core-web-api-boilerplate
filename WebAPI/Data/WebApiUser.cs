using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data
{
    public class WebApiUser : IdentityUser
    {
        [StringLength(3)]
        public string SomeExtraSmallField { get; set; }
    }
}
