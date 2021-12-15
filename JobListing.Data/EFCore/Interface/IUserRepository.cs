using JobListing.Data.Interface;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.EFCore.Interface
{
    public interface IUserRepository:ICRUDRepo
    {

        Task<List<AppUser>> GetUsers();

        Task<AppUser> GetUserByEmail(string email);

        Task<AppUser> GetUserById(string id);

    }
}
