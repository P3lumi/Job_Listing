using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class UploadCvDto
    {
        [Required]
        public IFormFile Cv { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; }

    }
}
