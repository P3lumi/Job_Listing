using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobListing.Core.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(UserDto user);
        Task<List<User>> GetUsers();
        Task<int> DeleteUser();
    }
}
