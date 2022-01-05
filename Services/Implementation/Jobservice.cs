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
        private readonly ICvRepository _cvRepo;
        private readonly IMapper _mapper;

        public JobService(IJobRepository jobRepository, ICvRepository cvRepository, IMapper mapper)
        {
            _jobRepo = jobRepository;
            _cvRepo = cvRepository;
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
            bool isCategoryExist = false;
            string categoryId = "";
            bool isIndustryExist = false;
            string industryId = "";
            var cat =await _jobRepo.GetCategories();
            foreach(var item in cat)
            {
              
                if (item.Name.ToLower() == job.category.ToLower())
                {
                    isCategoryExist = true;
                    categoryId = item.Id;
                }
                    
            }
            if (isCategoryExist == false)
                return false;

            var ind = await _jobRepo.GetIndustries();
 
            foreach (var item in ind)
            {
                if (item.IndustryName.ToLower() == job.industry.ToLower())
                {
                    isIndustryExist = true;
                    industryId = item.Id;
                }
                    
            }
            if (isIndustryExist == false)
                return false;

            var j = new Job();


            j.StartPrice = job.StartPrice;
            j.EndPrice = job.EndPrice;
            j.Title = job.JobTitle;
            j.CategoryId = categoryId;
            j.IndustryId = industryId;
            


           // var data = _mapper.Map<Job>(job);

            var response = await _jobRepo.Add(j);
            if (response == false)
                return false;
            return true;
        }

        //public Task ApplyForJob()
        //{
        //    throw new NotImplementedException();
        //}

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

        public async Task<List<JobToReturnDto>> GetJobByCategory(string category)
        {
            
            try
            {

                string categoryId = "";
                var cat = await _jobRepo.GetCategories();
                foreach (var item in cat)
                {

                    if (item.Name.ToLower() == category.ToLower())
                    {
                        categoryId = item.Id;
                        break;
                    }

                }

                var result = new List<JobToReturnDto>();
                var job = await _jobRepo.GetJobByCategory(categoryId);
                foreach(var item in job)
                {
                    result.Add(_mapper.Map<JobToReturnDto>(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }

        public async Task<List<JobToReturnDto>> GetJobByIndustry(string industry)
        {
            
            try
            {
                string industryId = "";
                var cat = await _jobRepo.GetIndustries();
                foreach (var item in cat)
                {

                    if (item.IndustryName.ToLower() == industry.ToLower())
                    {
                        industryId = item.Id;
                        break;
                    }

                }

                var result = new List<JobToReturnDto>();
                var job = await _jobRepo.GetJobByIndustry(industryId);
                foreach (var item in job)
                {
                    result.Add(_mapper.Map<JobToReturnDto>(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
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
