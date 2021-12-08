using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Enums
{
    public enum JobNature
    {
        [Display(Name = "Full Time")]
        FullTime,
        [Display(Name = "Part Time")]
        PartTime,
        Contract,
        Internship
    }
}
