using AutoMapper;
using JobListing.Common;
using JobListing.Data.EFCore.Interface;
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
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepo = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddUser(UserDto user)
        {
            //User data = new User();
            //var res = Util.HashGenerator(user.Password);
            //data.Email = user.Email;
            //data.Firstname = user.Firstname;
            //data.Lastname = user.Lastname;
            //data.PasswordSalt = res[0];
            //data.PasswordHash = res[1];

            var data = _mapper.Map<AppUser>(user);

            var response = await _userRepo.Add(data);
            if (response == false)
                return false;
            return true;
        }

        public async Task<bool> DeleteUser(string userId)
        {

            bool response = false;

            try
            {
                if (await _userRepo.Delete(userId))
                {
                    response = true;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
            }

       

        public async Task<AppUser> EditUser(UserDto user)
        {
             AppUser editedUser = null;
            try
            {
                var userFromDb = await _userRepo.GetUserByEmail(user.Email);
                if (userFromDb != null)
                {
                    // map new details to user fetched
                   
                    userFromDb.LastName = user.Lastname;
                    userFromDb.FirstName = user.Lastname;
                    userFromDb.Email = user.Email;
                }

                if (await _userRepo.Edit<UserDto>(user))
                {
                    editedUser = new AppUser
                    {
                        
                        FirstName = user.Firstname,
                        LastName = user.Lastname,
                        Email = user.Email
                    };
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return editedUser;

            

        }


        public async Task<AppUser> GetUserById(string id)
        {
            var user = new AppUser();
            try
            {
                user = await _userRepo.GetUserById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        
        public async Task<AppUser> GetUserByEmail(string email)
        {
           var user = new AppUser();
            try
            {
                user = await _userRepo.GetUserByEmail(email);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;

        }


        public async Task<List<UserToReturnDto>> GetUsers()
        {
            
            try
            {
               var user = await _userRepo.GetUsers();
                var response = new List<UserToReturnDto>();
                if (user != null)
                {
                    foreach(var item in user) {

                        //new UserToReturnDto
                        //{
                        //    Email = item.Email,
                        //    Firstname = item.FirstName,
                        //    Lastname = item.LastName,


                        //}
                        var mapper = _mapper.Map<UserToReturnDto>(item);
                        response.Add(mapper);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }




    }
}
