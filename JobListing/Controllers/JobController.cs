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

        [HttpPost("Add_Job")]
        public IActionResult AddJob()
        {

            return Ok();
        }




        [HttpGet("Search_by_name")]
        public IActionResult SearchJobByName()
        {

            return Ok();
        }


        [HttpGet("Search_by_category")]
        public IActionResult SearchJobByCategory()
        {

            return Ok();
        }



        //[HttpPost("Upload_CV")]
        //public IActionResult UploadCv()
        //{

        //    return Ok();
        //}



        [HttpDelete("Delete _job")]
        public IActionResult DeleteJob()
        {

            return Ok();
        }


    }
}
