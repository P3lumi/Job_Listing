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
    public class UserController : ControllerBase
    {

                

        [HttpGet("View profile")]
        public IActionResult ViewProfile()
        {

            return Ok();
        }


        [HttpPatch("Edit Profile")]
        public IActionResult EditProfile()
        {

            return Ok();
        }


        [HttpPost("Deactivate Profile")]
        public IActionResult DeactivateProfile()
        {

            return Ok();
        }


        [HttpPost("Reactivate Profile")]
        public IActionResult ReactivateProfile()
        {

            return Ok();
        }


    }
}
