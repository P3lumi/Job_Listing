using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class JobDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Title must not be less than 3 charcaters and more than 20 characters")]
        [Display(Name ="Job Title")]
        public string JobTitle { get; set; }


        [Required] 
        [Display(Name ="Job Description")]
        public string JobDescription { get; set; }


        [Required]
        public double StartPrice { get; set; }


        [Required]
        public double EndPrice { get; set; }


        [Required]
        public Category category { get; set; }


        [Required]
        public Industry industry { get; set; }
    }
}
