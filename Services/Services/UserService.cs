using JobListing.Data.Repositories.Database;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobListing.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        public async Task<bool> AddUser(UserDto user)
        {
            User data = new User();
            data.Email = user.Email;
            data.Firstname = user.Firstname;
            data.Lastname = user.Lastname;
            data.PasswordSalt = user.Password;
            data.PasswordHash = "sdsfsd";

            var response = await _userRepo.Add(data);
            if (response == false)
                return false;
            return true;
        }

        public async Task<List<User>> GetUsers()
        {
            var response = await _userRepo.GetUsers();
            return response;
        }
    }
}
