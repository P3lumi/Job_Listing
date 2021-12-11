using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Core.Services
{
    public class Jobservice : IJobService
    {
        public Task<bool> AddJob()
        {
            throw new NotImplementedException();
        }

        public Task ApplyForJob()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteJob()
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetJob()
        {
            throw new NotImplementedException();
        }

        public Task SearchJob()
        {
            throw new NotImplementedException();
        }

        public Task UploadCv()
        {
            throw new NotImplementedException();
        }
    }
}
