using JobListing.Data.Interface;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.Repositories.Database
{
    public interface IUserRepository: ICRUDRepo 
    {
        Task<bool> Add<T>(T entity);
        Task<List<User>> GetUsers();

        Task<int> DeleteUser();

    }
}
