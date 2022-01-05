using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AppUser: IdentityUser
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public List<Job> Jobs { get; set; }
        public List<Cv> Cvs { get; set; }


        public AppUser()
        {
            Jobs = new List<Job>();
            Cvs = new  List< Cv>();
        }
    }
}
