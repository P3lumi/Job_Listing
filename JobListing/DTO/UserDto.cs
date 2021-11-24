using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobListing.UI.DTO
{
    public class UserDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 charcaters and more than 20 characters")]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 charcaters and more than 20 characters")]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
