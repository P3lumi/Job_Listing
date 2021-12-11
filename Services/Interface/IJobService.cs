using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Core.Services
{
    interface IJobService
    {
        Task<bool> AddJob();
        Task<List<Job>> GetJob();
        Task<int> DeleteJob();

        Task SearchJob();

        Task ApplyForJob();

        Task UploadCv();


    }
}
