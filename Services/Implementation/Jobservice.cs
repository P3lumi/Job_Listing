using AutoMapper;
using JobListing.Data.EFCore.Interface;
//using JobListing.Data.Repositories.Database;
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
        private readonly IMapper _mapper;

        public JobService(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepo = jobRepository;
            _mapper = mapper;
        }
        public List<Job> Jobs
        {
            get
            {
                return _jobRepo.GetJobs().Result;
            }
        }

        public async Task<bool> AddJob(JobDto job)
        {
            var data = _mapper.Map<Job>(job);

            var response = await _jobRepo.Add(data);
            if (response == false)
                return false;
            return true;
        }

        public Task ApplyForJob()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteJob(string jobId)
        {
            bool response = false;

            try
            {
                if (await _jobRepo.Delete(jobId))
                {
                    response = true;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        public Task<Job> EditJob(JobDto job)
        {
            throw new NotImplementedException();
        }

        public async Task<Job> GetJobByCategory(string category)
        {
            var job = new Job();
            try
            {
                job = await _jobRepo.GetJobByCategory(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return job;
        }

        public async Task<Job> GetJobByIndustry(string industry)
        {
            var job = new Job();
            try
            {
                job = await _jobRepo.GetJobByIndustry(industry);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return job;
        }

        public async Task<List<JobToReturnDto>> GetJobs()
        {
            try
            {
                var job = await _jobRepo.GetJobs();
                var response = new List<JobToReturnDto>();
                if (job != null)
                {
                    foreach (var item in job)
                    {

                      
                        var mapper = _mapper.Map<JobToReturnDto>(item);
                        response.Add(mapper);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Task UploadCv()
        {
            throw new NotImplementedException();
        }




    }
}
