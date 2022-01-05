using JobListing.Common;
using JobListing.Core.Interface;
using JobListing.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Models.EmailModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobListing.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // private readonly IJwtService _jwt;
        // private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userMgr;
        private readonly IEmailService emailService;

        public AuthController(IAuthService auth, UserManager<AppUser> userManager,IEmailService emailService)
        {
            //_jwt = jwt;
            //  _userService = userService;
            _userMgr = userManager;
            this.emailService = emailService;
            _authService = auth;

        }

        [HttpPost ("Login")]

        public async Task<IActionResult> Login(LogInDto model)
        {

            var user = await _userMgr.FindByEmailAsync(model.email);
            if(user == null)
            {
                ModelState.AddModelError("Invalid", "Credentials provided by the user is invalid");
                return BadRequest(Util.BuildResponse<object>(false, "Invalid Credentials", ModelState, null));
            }

            //check if user email is confirmed
            if(await _userMgr.IsEmailConfirmedAsync(user))
            {
                var res = await _authService.Login(model.email, model.password, model.RememberMe);
                if (!res.status)
                {
                    ModelState.AddModelError("Invalid", "Credentials provided by the user is invalid");
                    return BadRequest(Util.BuildResponse<object>(false, "Invalid Credentials", ModelState, null));
                }

                return Ok(Util.BuildResponse(true, "Log in is successful", null, res));

            }

            ModelState.AddModelError("Invalid", "User must first confirm email before attempting log in");
            return BadRequest(Util.BuildResponse<object>(false, "Email not confirmed", ModelState, null));

          //  return Ok(_userService.GetUserByEmail(email.email));


        }

        [HttpPost("Email_Notification")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await  emailService.SendMailAsync(request);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }
        

        [HttpPost("Logout")]
        public IActionResult Logout()
        {

            return Ok();
        }


        //[HttpPost("Register")]
        //public IActionResult Register(UserDto details)
        //{
        //    var response = _userService.AddUser(details);
        //    return Ok();
        //}


        //[HttpPost("Forget Password")]
        //public IActionResult ForgetPassword()
        //{

        //    return Ok();
        //}



    }
}
