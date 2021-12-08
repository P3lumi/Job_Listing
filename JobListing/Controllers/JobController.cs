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
    public class JobController : ControllerBase
    {

        [HttpPost("Add Job")]
        public IActionResult AddJob()
        {

            return Ok();
        }


        [HttpGet("Job Description")]
        public IActionResult ViewJobDescription()
        {

            return Ok();
        }


        [HttpGet("Search by name")]
        public IActionResult SearchJobByName()
        {

            return Ok();
        }


        [HttpGet("Search by category")]
        public IActionResult SearchJobByCategory()
        {

            return Ok();
        }



        [HttpPost("Upload CV")]
        public IActionResult UploadCv()
        {

            return Ok();
        }



        [HttpDelete("Delete Job")]
        public IActionResult DeleteJob()
        {

            return Ok();
        }


    }
}
