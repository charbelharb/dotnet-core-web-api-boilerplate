using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data
{
    public class WebApiUser : IdentityUser
    {
        [StringLength(3)]
        public string SomeExtraSmallField { get; set; }
    }
}
