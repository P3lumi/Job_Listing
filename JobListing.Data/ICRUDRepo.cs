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
        Task<bool> Add<T>(T entity, string role="role");
        Task<bool> Edit<T>(T entity);
        Task<bool> Delete<T>(T entity);
        
        


    }
}
