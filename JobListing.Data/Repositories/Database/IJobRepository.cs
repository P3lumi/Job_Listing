using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.Repositories.Database
{
    public interface IJobRepository
    {

        Task<List<Job>> GetJobs();
        Task<Job> GetUserByCategory(string category);
        Task<Job> GetUserByIndustry(string industry);

    }
} 
