using JobListing.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobListing.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IService _jwt;

        public AuthorizationController(IService jwt)
        {
            _jwt = jwt;
        }

        [HttpGet ("Login")]

        public IActionResult Login()
        {
            return Ok(_jwt.GenerateToken());
        }

        [HttpGet]
        [Authorize]
        public IActionResult Authorize()
        {
            return Ok("Authorized");
        }
    }
}
