using JobListing.Common;
using JobListing.Core.Interface;
using JobListing.Core.Services;
using JobListing.Data.EFCore;
using JobListing.Data.EFCore.Interface;
using Microsoft.AspNetCore.Identity;
using Models;
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

       // private readonly IUserRepository _userRepo;
        private readonly IJwtService _jWTService;
        private readonly UserManager<AppUser> _userMgr;
        private readonly SignInManager<AppUser> _signinMgr;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager, IJwtService jWTService)
        {
            //_userRepo = userRepository;
            _jWTService = jWTService;
            _userMgr = userManager;
            _signinMgr = signinManager;


        }
        public async Task<LogInCredDto> Login(string email, string password, bool rememberMe)
        {
            var res = new LogInCredDto();
           
            var user = await _userMgr.FindByEmailAsync(email);
            if (user==null)
            {
                res.status = false;
                return res;
            }
            else
            {
                var result = await _signinMgr.PasswordSignInAsync(user, password, rememberMe, false);
                
                

                if(result == null )
                {
                    res.status = false;
                    return res;
                }

                else if (!result.Succeeded)
                {
                     res.status = false ;
                    return res;
                }

            }
            

            //get jwt token
            var userRoles = await _userMgr.GetRolesAsync(user);
            var token = _jWTService.GenerateToken(user, userRoles.ToList());

            return new LogInCredDto { status = true, Id = user.Id, token = token };






            //commented ado
            // the code below has no much importance now that LoginDto have been added data annotations
            //#region removable code
            //if (String.IsNullOrWhiteSpace(email))
            //    throw new Exception("Email is empty");
            //if (String.IsNullOrWhiteSpace(password))
            //    throw new Exception("Password is empty");
            //#endregion

            //var loginCred = new LogInCredDto();
            //var res = new ResponseDto<LogInCredDto>();
            //List<string> roles = new List<string>();
            //roles.Add("admin");
            //try
            //{
            //    var response = await _userRepo.GetUserByEmail(email);
            //   // if (Util.CompareHash(password, response.PasswordHash, response.PasswordSalt))
            //   // {
            //        //loginCred.Id = response.Id;
            //        loginCred.token = _jWTService.GenerateToken(response, roles);

            //        res.Status = true;
            //        res.Message = "Login sucessfully!";
            //        res.Data = loginCred;
            //   // }
            //    //else
            //    //{
            //    //    res.Status = false;
            //    //    res.Message = "Login failed!";
            //    //    res.Data = null;
            //    //}

            //}
            //catch (Exception e)
            //{
            //    //Log error
            //    throw new Exception(e.Message);
            //}
            //return res;

        }
    }
}
