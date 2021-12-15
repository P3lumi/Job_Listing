using JobListing.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobListing.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly ILogger<UserController> logger;
        private ILogger<UserController> _logger;
        private readonly IUserService _userService; 
        private readonly UserManager<AppUser> _userMgr;



        public UserController(ILogger<UserController> logger, IUserService userService,
            UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userService = userService;
            _userMgr = userManager;
        }


        [HttpGet("get_users")]
        public IActionResult GetUsers()
        {
            //map data from db to reshape it and remove null fields
            

            var response = _userService.GetUsers();
            return Ok(response);
        }



        [HttpGet("View_profile")]
        public async Task< IActionResult> ViewProfile(string email)
        {

            var response = await _userService.GetUserByEmail(email);
            return Ok(response);

        }


        [HttpPatch("Edit_Profile")]
        public IActionResult EditProfile(UserDto user)
        {

            var response = _userService.EditUser(user);
            return Ok(response);
        }


        [HttpPost("Deactivate_Profile")]
        public IActionResult DeactivateProfile(string email)
        {

            var response = _userService.DeleteUser(email);
            return Ok(response);
        }


        [HttpPost("Reactivate_Profile")]
        public IActionResult ReactivateProfile(UserDto user)
        {

            var response = _userService.AddUser(user); 
            return Ok(response);
        }


    }
}
