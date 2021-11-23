using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class Job
    {
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string JobDescription { get; set; }
        [Required]
        public double StartPrice { get; set; }
        [Required]
        public double EndPrice { get; set; }
        public Category category { get; set; }
        public Industry industry { get; set; }
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
        //enum Industry
        //{
        //    Education,
        //    Fashion,
        //    Technology,
        //    Healthcare,
        //    Agriculture,
        //    Hospitality,
        //    Tourism,
        //    Telecommunication,
        //    Engineering,
        //    Advertising,
        //    Media,
        //    Marketing,
        //    Entertainment,
        //    Finance,
        //    Food
        //}
    }
}
