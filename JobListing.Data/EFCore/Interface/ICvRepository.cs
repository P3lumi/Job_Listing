using JobListing.Data.Interface;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.EFCore.Interface
{
    public interface ICvRepository: ICRUDRepo
    {
        Task<bool> SaveChanges();
        Task<List<Cv>> GetCvs();
        Task<Cv> GetCvByPublicId(string PublicId);
        Task<List<Cv>> GetCvByUserId(string UserId);

    }
}
