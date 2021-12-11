using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.Interface
{
   public interface ICRUDRepo
    {
        Task<bool> Add<T>(T entity);
        Task<List<User>> GetUsers();
        Task<int> Delete(string userid);
        


    }
}
