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
        Task<List<Job>> GetJobByCategory(string category);
        Task<List<Job>> GetJobByIndustry(string industry);

        Task<List<Category>> GetCategories();
        Task<List<Industry>> GetIndustries();
    }
}
