﻿using JobListing.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
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
        private readonly IJwtService _jwt;
        private readonly IUserService _userService;

        public AuthController(IJwtService jwt,IUserService userService)
        {
            _jwt = jwt;
            _userService = userService;
        }

        [HttpPost ("Login")]

        public IActionResult Login(LogInDto email)
        {
            
            return Ok(_userService.GetUserByEmail(email.email));
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {

            return Ok();
        }


        [HttpPost("Register")]
        public IActionResult Register(UserDto details)
        {
            var response = _userService.AddUser(details);
            return Ok();
        }


        //[HttpPost("Forget Password")]
        //public IActionResult ForgetPassword()
        //{

        //    return Ok();
        //}



    }
}