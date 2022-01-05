using AutoMapper;
using CloudinaryDotNet.Actions;
using JobListing.Common;
using JobListing.Core.Interface;
using JobListing.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;
using Models.DTO;
using Models.JobDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobListing.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IJobService _jobService;
        private readonly ICvService _cvService;

        public JobController(IMapper mapper, IJobService jobService, ICvService cvService)
        {
            _mapper = mapper;
            _jobService = jobService;
            _cvService = cvService;

        }


        [HttpPost("Add_Job")]
        public async Task<IActionResult> AddJob(JobDto job)
        {
           
             var response = await _jobService.AddJob(job);

            if(!response)
            {
                ModelState.AddModelError("Failed", "Job not added");
                return BadRequest(Util.BuildResponse<string>(false, "Job not added", ModelState, null));
            }


            return Ok(Util.BuildResponse<string>(true, "Job added successfully", ModelState, null));
        }

        [HttpGet("Getjobs")]
        public async Task<IActionResult> GetJobs()
        {
            var response = await _jobService.GetJobs();
            return Ok(response);
        }


        [HttpGet("Search_by_industry")]
        public async Task<IActionResult> GetJobByIndustry(string industry)
        {

            var response = await _jobService.GetJobByIndustry(industry);

            if (response == null)
            {
                ModelState.AddModelError("Failed", "Job not found");
                return BadRequest(Util.BuildResponse<string>(false, "Job not found", ModelState, null));
            }

            return Ok(Util.BuildResponse<List<JobToReturnDto>>(true, "Job found", ModelState, response));
        }


        [HttpGet("Search_by_category")]
        public async Task<IActionResult> GetJobByCategory(string category)
        {

            var response = await _jobService.GetJobByCategory(category);

            if(response == null)
            {
                ModelState.AddModelError("Failed", "Job not found");
                return BadRequest(Util.BuildResponse<string>(false, "Job not found", ModelState, null));
            }

            return Ok(Util.BuildResponse<List<JobToReturnDto>>(true, "Job found", ModelState, response));
        }


        [HttpDelete("Delete_job")]
        public async Task<IActionResult> DeleteJob(string jobId)
        {

            var response = await _jobService.DeleteJob(jobId);

            if (!response)
            {
                ModelState.AddModelError("Failed", "Job not deleted");
                return BadRequest(Util.BuildResponse<string>(false, "Job not deleted", ModelState, null));
            }

            return Ok(Util.BuildResponse<string>(true, "Job deleted", ModelState, null));
        }


        [HttpPost("Upload_Cv")]
        public async Task<IActionResult> UploadCv([FromForm] UploadCvDto model, string userId)
        {

            var file = model.Cv;
            if(file.Length > 0)
            {
                var uploadStatus = await _cvService.UploadCvAsync(model, userId);

                if (uploadStatus.Item1)
                {
                    var res = await _cvService.AddCvAsync(model, userId);
                    if (!res.Item1)
                    {
                        ModelState.AddModelError("Failed", "Could not add cv to database");
                        return BadRequest(Util.BuildResponse<RawUploadResult>(false, "Fail to add to database", ModelState, null));
                    }

                    return Ok(Util.BuildResponse<object>(true, "Upload successfull", null, new { res.Item2.PublicId, res.Item2.Url }));
                }

                ModelState.AddModelError("Failed", "File could not be uploaded to cloudinary");
                return BadRequest(Util.BuildResponse<RawUploadResult>(false, "Failed to upload", ModelState, null));

            }
            ModelState.AddModelError("Invalid", "File size must not be empty");
            return BadRequest(Util.BuildResponse<RawUploadResult>(false, "File is empty", ModelState, null));


        }


        [HttpDelete("deletecv")]
        public async Task<IActionResult> DeleteCv (string userId, string publicId)
        {
            var res = await _cvService.DeleteCvAsync(publicId);
            if (!res)
            {
                ModelState.AddModelError("Failed", "Could not delete photo");
                return BadRequest(Util.BuildResponse<ImageUploadResult>(false, "Delete failed!", ModelState, null));
            }

            return Ok(Util.BuildResponse<string>(true, "Photo deleted sucessful!", null, ""));
        }

            


    }
}
