using JobListing.Data.Repositories.Database;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.Implementation
{
    public class JobRepository : IJobRepository
    {

        private readonly IADOOperations _ado;
        private readonly SqlConnection _conn;
        private readonly IConfiguration _config;

        public JobRepository(IADOOperations aDOOperations, IConfiguration config)
        {
            _ado = aDOOperations;
            _conn = new SqlConnection(config.GetSection("ConnectionStrings:Default").Value);
            _config = config;
        }

        
        public Task<List<Job>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteJob(string jobid)
        {
            throw new NotImplementedException();
        }

      
    }
}
