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

        Task<bool> DeleteUser(string userId);
        Task<List<UserToReturnDto>> GetUsers();
        Task<AppUser> GetUserByEmail(string email);
        Task<AppUser> GetUserById(string id); 
        Task<AppUser> EditUser(UserDto user);
        

    }
}
