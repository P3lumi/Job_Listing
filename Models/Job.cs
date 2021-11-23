using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class Job
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public double StartPrice { get; set; }
        public double EndPrice { get; set; }
        public string SalaryRange
        {
            get
            {
                return $"{StartPrice} to {EndPrice}";
            }
        }

        enum JobType
        { 
            FullTime,
            PartTime,
            Contract,
            Internship,
            Freelance

        }

        enum Location
        {
            Abia,
            Abuja,
            Adamawa,
            AkwaIbom,
            Anambra,
            Bauchi,
            Bayelsa,
            Benue,
            CrossRiver,
            Delta,
            Ebonyi,
            Edo,
            Ekiti,
            Enugu,
            Gombe,
            Imo,
            Jigawa,
            Kaduna,
            Kano,
            Katsina,
            Kebbi,
            Kogi,
            Kwara,
            Lagos,
            Nasarawa,
            Niger,
            Ogun,
            Ondo
                    
        }
        enum Industry
        {
            Education,
            Fashion,
            Technology,
            Healthcare,
            Agriculture,
            Hospitality,
            Tourism,
            Telecommunication,
            Engineering,
            Advertising,
            Media,
            Marketing,
            Entertainment,
            Finance,
            Food
        }
    }
}
