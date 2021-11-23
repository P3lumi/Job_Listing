using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
       
        public string UserId { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


        public User()
        {
            this.UserId = Guid.NewGuid().ToString();
        }
    }
}
