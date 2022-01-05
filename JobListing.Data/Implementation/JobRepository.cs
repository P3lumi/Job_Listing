//using JobListing.Data.Repositories.Database;
//using Microsoft.Extensions.Configuration;
//using Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JobListing.Data.Implementation
//{
//    public class JobRepository : IJobRepository
//    {

//        private readonly IADOOperations _ado;
//        private readonly SqlConnection _conn;
//        private readonly IConfiguration _config;

//        public JobRepository(IADOOperations aDOOperations, IConfiguration config)
//        {
//            _ado = aDOOperations;
//            _conn = new SqlConnection(config.GetSection("ConnectionStrings:Default").Value);
//            _config = config;
//        }


//        public async Task<bool> Add<T>(T entity)
//        {
//            var job = entity as Job;
            
//            var stmt = $"INSERT INTO [job] (JobId, JobTitle, SalaryRange, CategoryId, Category, IndustryId, Industry)" +
//                        $"VALUES('{job.Id}', '{job.Title}', '{job.SalaryRange}', '{job.CategoryId}', {job.category}, {job.IndustryId}, {job.industry})";
//            try
//            {
//                if (await _ado.ExecuteForQuery(stmt))
//                {
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }

//            return false;
//        }


//        public async Task<bool> Delete<T>(T entity)
//        {
//            var job = entity as Job;

//            var stmt = $"DELETE FROM [job] WHERE JobId = '{job.Id}' OR Category = '{job.category}' OR Industry = '{job.industry}";
//            try
//            {
//                if (await _ado.ExecuteForQuery(stmt))
//                {
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }

//            return false;
//        }

//        public async Task<bool> Edit<T>(T entity)
//        {
//            var job = entity as Job;

//            var stmt = $"UPDATE [job] SET JobId = '{job.Id}', JobTitle = '{job.Title}', " +
//                $"SalaryRange = '{job.SalaryRange}', Category = '{job.category}', Industry ='{job.industry}" +
//                $"WHERE id = '{job.Id}' OR Category = '{job.category}' OR Industry = '{job.industry}' ";

//            try
//            {
//                if (await _ado.ExecuteForQuery(stmt))
//                {
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }

//            return false;
//        }




//        public async Task<List<Job>> GetJobs()
//        {
//            var listOfJobs = new List<Job>();

//            string stmt = $"SELECT * FROM {_config.GetSection("Tables:jobTable").Value}";

//            try
//            {
//                var response = await _ado.ExecuteForReader(stmt, "JobId", "JobTitle", "SalaryRange", "Category", "Industry");

//                if (response.Count <= 0)
//                {
//                    throw new Exception("No record found");
//                }

//                foreach (var item in response)
//                {
//                    //var values = item.Values.ToArray();

//                    listOfJobs.Add(new Job
//                    {
//                        Id = item.Values[0],
//                        Title = item.Values[1],
//                        //SalaryRange = item.Values[2],
//                        //category = item.Values[3],
//                        //industry = item.Values[4]
//                    });
//                }

//            }
//            catch (DbException ex)
//            {
//                throw new Exception(ex.Message);
//            }

//            return listOfJobs;
//        }

//        public async Task<Job> GetUserByCategory(string category)
//        {
//            var job = new Job();

//            string stmt = $"SELECT * FROM {_config.GetSection("Tables:jobTable").Value} WHERE Category = '{category}'";

//            try
//            {
//                var response = await _ado.ExecuteForReader(stmt, "JobId", "JobTitle", "SalaryRange", "Category", "Industry");

//                if (response.Count <= 0)
//                {
//                    return null;
//                }

//                job = new Job
//                {
//                    Id = response[0].Values[0],
//                    Title = response[0].Values[1],
//                    //SalaryRange = response[0].Values[2],
//                   // category = response[0].Values[3],
//                   // industry = response[0].Values[4],
//                };

//            }
//            catch (DbException ex)
//            {
//                throw new Exception(ex.Message);
//            }

//            return job;
//        }

//        public Task<Job> GetUserByIndustry(string industry)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
