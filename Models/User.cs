using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User: BaseEntity
    {

        //public string UserId { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string PasswordSalt { get; set; }



        public List<Role> roles { get; set; }

        public User()
        {
           
            this.roles = new List<Role>();

        }




    
    }
}
