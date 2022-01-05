using JobListing.Data.EFCore.Interface;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.EFCore.Implementation
{
    public class JobRepository : IJobRepository
    {
        private readonly JobListingContext context;

        public JobRepository(JobListingContext context)
        {
            this.context = context;
        }
       

        public async Task<bool> Add<T>(T entity, string role = "role")
        {
            var job = entity as Job;
            var add = await context.Job.AddAsync(job);
            await context.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete<T>(T entity)
        {
            var job = entity as Job;
            var delete = context.Job.Remove(job);

            return Task.FromResult(true);
        }

        public Task<bool> Edit<T>(T entity)
        {
            var job = entity as Job;
            var edit = context.Job.Update(job);
            context.SaveChanges();

            return Task.FromResult(true);
        }

        public Task<List<Job>> GetJobs()
        {
          
            var get = context.Job.ToList();
            var query = from j in context.Job
                        join c in context.Category on j.CategoryId equals c.Id
                        join i in context.Industry on j.IndustryId equals i.Id
                        select new
                        {
                            j.Id,
                            j.StartPrice,
                            j.EndPrice,
                            j.Title,
                            c.Name,
                            i.IndustryName,
                        };
            List<Job> result = new List<Job>();
            foreach(var item in query)
            {
                result.Add(new Job
                {
                    Id = item.Id,
                    StartPrice = item.StartPrice,
                    EndPrice = item.EndPrice,
                    Title = item.Title,
                    Category = new Category {
                        Name = item.Name},
                    Industry = new Industry { IndustryName = item.IndustryName}
                });
            }
           return Task.FromResult(result);
        }

        public Task<List<Job>> GetJobByCategory(string categoryId)
        {

            var res = context.Job.Where(x => x.CategoryId == categoryId).ToList();

            return Task.FromResult(res);
            //var res = from j in context.Job join c in context.Category on j.CategoryId equals c.Id where c.Name == category select new { 

            //    j.Id,
            //    j.Title,
            //    j.StartPrice,
            //    j.EndPrice,
            //    j.industry,

            //};
            //var result = new Job();
            //foreach(var item in res)
            //{
            //    result.Id = item.Id;
            //    result.Title = item.Title;
            //    result.StartPrice = item.StartPrice;
            //    result.EndPrice = item.EndPrice;
            //    result.industry = item.industry;
            //}
        }

        public Task<List<Job>> GetJobByIndustry(string industryId)
        {
            var res = context.Job.Where(x => x.IndustryId == industryId).ToList();
            return Task.FromResult(res);
            //var res = from j in context.Job
            //          join i in context.Industry on j.IndustryId equals i.Id
            //          where i.Name == industry
            //          select new
            //          {

            //              j.Id,
            //              j.Title,
            //              j.StartPrice,
            //              j.EndPrice,
            //              j.category,


            //          };
            //var result = new Job();
            //foreach (var item in res)
            //{
            //    result.Id = item.Id;
            //    result.Title = item.Title;
            //    result.StartPrice = item.StartPrice;
            //    result.EndPrice = item.EndPrice;
            //    result.category = item.category;

            //}
            
        }

        public async Task<List<Category>> GetCategories()
        {
            var res = context.Category.ToList();
            return res;
        }

        public async Task<List<Industry>> GetIndustries()
        {
            var res = context.Industry.ToList();
            return res;
        }
    }
}
