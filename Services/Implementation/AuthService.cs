using JobListing.Common;
using JobListing.Core.Interface;
using JobListing.Core.Services;
using JobListing.Data.EFCore;
using JobListing.Data.EFCore.Interface;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Core.Implementation
{
    public class AuthService:IAuthService
    {

        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jWTService;

        public AuthService(IUserRepository userRepository, IJwtService jWTService)
        {
            _userRepo = userRepository;
            _jWTService = jWTService;
        }
        public async Task<ResponseDto<LogInCredDto>> Login(string email, string password)
        {
            // the code below has no much importance now that LoginDto have been added data annotations
            #region removable code
            if (String.IsNullOrWhiteSpace(email))
                throw new Exception("Email is empty");
            if (String.IsNullOrWhiteSpace(password))
                throw new Exception("Password is empty");
            #endregion

            var loginCred = new LogInCredDto();
            var res = new ResponseDto<LogInCredDto>();
            List<string> roles = new List<string>();
            roles.Add("admin");
            try
            {
                var response = await _userRepo.GetUserByEmail(email);
               // if (Util.CompareHash(password, response.PasswordHash, response.PasswordSalt))
               // {
                    //loginCred.Id = response.Id;
                    loginCred.token = _jWTService.GenerateToken(response, roles);

                    res.Status = true;
                    res.Message = "Login sucessfully!";
                    res.Data = loginCred;
               // }
                //else
                //{
                //    res.Status = false;
                //    res.Message = "Login failed!";
                //    res.Data = null;
                //}

            }
            catch (Exception e)
            {
                //Log error
                throw new Exception(e.Message);
            }
            return res;

        }
    }
}
