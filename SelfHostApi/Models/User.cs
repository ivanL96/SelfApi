using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string AccessToken { get; set; }
    }
}
