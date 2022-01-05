using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JobDto
{
   public class JobToReturnDto
    {

        public string Title { get; set; }
        public string SalaryRange { get; set; }

        public string StartPrice { get; set; }

        public string EndPrice { get; set; }
        public string categoryName { get; set; }
        public string industryName { get; set; }

    }
}
