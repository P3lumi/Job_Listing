using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AppUser: IdentityUser
    {
 
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string PassWord { get; set; }
        public bool IsActive { get; set; }
        List<Job> jobs = new List<Job>();
        

    }
}
