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
        
      
        Task<List<User>> GetUsers(string email);

        Task<List<User>> GetUserByEmail(string email);

        Task<List<User>> GetUserById(string id);


    }
}
