using AutoMapper;
using JobListing.Common;
using JobListing.Common.Helper;
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

        //private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userMgr;



        public UserController(ILogger<UserController> logger,
            UserManager<AppUser> userManager, IMapper  mapper)
        {
            _logger = logger;
           // _userService = userService;
            _userMgr = userManager;
            _mapper = mapper;
        }


        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser(RegisterDto model)
        {
            // check if user already exist

            var existingEmailUser = await _userMgr.FindByEmailAsync(model.Email);
            if(existingEmailUser != null)
            {
                ModelState.AddModelError("Invalid", $"User with email:{model.Email} already exists");
                return BadRequest(Util.BuildResponse<object>(false, "User already exists",  ModelState, null));
            }

            var user = _mapper.Map<AppUser>(model);

            var response = await _userMgr.CreateAsync(user, model.Password);

            if (!response.Succeeded)
            {
                foreach(var err in response.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(Util.BuildResponse<string>(false, "User not added", ModelState, null));
                   
            }

            var res = await _userMgr.AddToRoleAsync(user, "Applicant");
            if (!res.Succeeded)
            {
                foreach(var err in response.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(Util.BuildResponse<string>(false, "User not added", ModelState, null));
            }


            var token = await _userMgr.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action("ConfirmEmail", "User", new { Email = user.Email, Token = token }, Request.Scheme);



            var details = _mapper.Map<RegisterSuccessDto>(user);

            return Ok(Util.BuildResponse(true, "User added", null, new { details, ConfirmationLink = url }));

        }



        [HttpGet("get_users")]
        public IActionResult GetUsers(int page, int perPage)
        {

            var listOfUsersToReturn = new List<UserToReturnDto>();

            var users = _userMgr.Users.ToList();

            if(users != null)
            {
                var pagedList = PagedList<AppUser>.Paginate(users, page, perPage);
                foreach(var user in pagedList.Data)
                {
                    listOfUsersToReturn.Add(_mapper.Map<UserToReturnDto>(user));
                }

                var res = new PaginatedListDto<UserToReturnDto>
                {
                    MetaData = pagedList.MetaData,
                    Data = listOfUsersToReturn
                };

                return Ok(Util.BuildResponse(true, "Liat of users", null, res));
            }

            else
            {
                ModelState.AddModelError("Not Found", "No record found");
                var res = Util.BuildResponse<List<UserToReturnDto>>(false, "No result found", ModelState, null);
                return NotFound(res);
            }
            

            //ado
            //  //map data from db to reshape it and remove null fields

            //var response = _userService.GetUsers();
            //return Ok(response);
        }


        [HttpGet("get-user")]
        public async Task<IActionResult> GetUser(string email)
        {
            var UserToReturn = new UserToReturnDto();

            var user = await _userMgr.FindByEmailAsync(email);
            if(user != null)
            {
                UserToReturn = new UserToReturnDto
                {
                    Id = user.Id,
                    Lastname = user.Lastname,
                    Firstname = user.Firstname,
                    Email=user.Email
                };

                var res = Util.BuildResponse(true, "User Details", null, UserToReturn);
                return Ok(res);

            }
            else
            {
                ModelState.AddModelError("Not Found", $"No record found for user with email {user.Email}");
                return NotFound(Util.BuildResponse<List<UserToReturnDto>>(false, "No result found", ModelState, null));
            }
        }



        //[HttpGet("View_profile")]
        //public async Task< IActionResult> ViewProfile(string email)
        //{

        //    var response = await _userService.GetUserByEmail(email);
        //    return Ok(response);

        //}


        //[HttpPatch("Edit_Profile")]
        //public IActionResult EditProfile(UserDto user)
        //{

        //    var response = _userService.EditUser(user);
        //    return Ok(response);
        //}


        //[HttpPost("Deactivate_Profile")]
        //public async Task<IActionResult> DeactivateProfile(string email)
        //{

        //    var response = await _userService.DeleteUser(email);
        //    if(!response)
        //    {
        //        return BadRequest(new { Message = "User not deleted" });
        //    }
        //    return Ok(new { Message = "User succesfully deleted"});
        //}


        //[HttpPost("Reactivate_Profile")]
        //public IActionResult ReactivateProfile(UserDto user)
        //{

        //    var response = _userService.AddUser(user); 
        //    return Ok(response);
        //}


    }
}
