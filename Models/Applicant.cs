using Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Applicant
    {
        [Key]
        public string UserId { get; set; }
        public Locations Locations { get; set; }
        public string CurriculumVitae { get; set; }

    }

    
}
