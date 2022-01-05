using Models;
using Models.DTO;
using Models.JobDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Core.Services
{
    public interface IJobService
    {
        public List<Job> Jobs { get; }
        
        Task<bool> AddJob(JobDto job);
        Task<bool> DeleteJob(string jobId);
        Task<List<JobToReturnDto>> GetJobs();
        Task<List<JobToReturnDto>> GetJobByCategory(string category);
        Task<List<JobToReturnDto>> GetJobByIndustry(string industry);
        Task<Job> EditJob(JobDto job);

        // Task SearchJob();

       // Task ApplyForJob();

        Task UploadCv();


    }
}
