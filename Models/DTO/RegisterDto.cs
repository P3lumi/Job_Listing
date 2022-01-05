using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.DTO
{
   public class RegisterDto
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        // public IFormFile CV { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Please enter a valid email")]
        public string Email { get; set; }


        public string State { get; set; }

        public string Country { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }



    }
}
