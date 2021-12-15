using JobListing.Data.Repositories.Database;
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
    public class JobService : IJobService
    {

        private readonly IJobRepository _jobRepo;
        public JobService(IJobRepository jobRepository)
        {
            _jobRepo = jobRepository;
        }
        public List<Job> Jobs
        {
            get
            {
                return _jobRepo.GetJobs().Result;
            }
        }

        public Task<bool> AddJob(JobDto job)
        {
            throw new NotImplementedException();
        }

        public Task ApplyForJob()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteJob(string jobId)
        {
            throw new NotImplementedException();
        }

        public Task<Job> EditJob(JobDto job)
        {
            throw new NotImplementedException();
        }

        public Task<Job> GetJobByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Task<Job> GetJobByIndustry(string industry)
        {
            throw new NotImplementedException();
        }

        public Task<List<JobToReturnDto>> GetJobs()
        {
            throw new NotImplementedException();
        }

        public Task UploadCv()
        {
            throw new NotImplementedException();
        }


        //public async Task<bool> DeleteJob(JobDto job)
        //{
        //    var status = false;
        //    try
        //    {
        //        if (await _jobRepo.Delete<Job>(job))
        //        {
        //            status = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return status;
        //}


        //public async Task<List<Job>> GetJobs(string JobId)
        //{
        //    User job = null;
        //    try
        //    {
        //        job = await _jobRepo.GetJobs(JobId);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
        //    return job; throw new NotImplementedException();
        //}




    }
}
