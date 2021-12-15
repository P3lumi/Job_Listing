using JobListing.Data.Interface;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.EFCore.Interface
{
   public interface IJobRepository:ICRUDRepo
    {
        Task<List<Job>> GetJobs();
        Task<Job> GetJobByCategory(string category);
        Task<Job> GetJobByIndustry(string industry);

    }
}
