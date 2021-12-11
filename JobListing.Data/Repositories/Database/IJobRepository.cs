using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.Repositories.Database
{
    interface IJobRepository
    {
        
        Task<List<Job>> GetUsers();
        Task<int> DeleteJob(string jobid);

    }
}
