using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Job : BaseEntity
    {

        public string Title { get; set; }
       // public string Description { get; set; }


        public double StartPrice { get; set; }
        public double EndPrice { get; set; }

         
        public string CategoryId { get; set; }
        public Category category { get; set; }


        public string IndustryId { get; set; }
        public Industry industry { get; set; }


        public string SalaryRange
        {
            get
            {
                return $"{StartPrice} to {EndPrice}";
            }
        }

    }
}
       
