using JobListing.Core.Interface;
using JobListing.Core.Implementation;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.DTO;
using Moq;
using System;
using Xunit;
using JobListing.Core.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using JobListing.Data.EFCore;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace JobListing.Test
{
   
    public class AuthServiceTests
    {

     private IAuthService Service { get;  }
      
       
        //Arrange

        private const string email = "askjas@gmail.com";
        private const string pass = "P@ssw0rd";
        private const bool rem = false;

        public AuthServiceTests()
        {

            AppUser user = new AppUser
            {
                Id = "asdfghjkl",
                Email = "askjas@gmail.com",
                PasswordHash = "P@ssw0rd",
                UserName = "email",
                Firstname = "peks",
                Lastname = "pees"
            };

            List<string> roles = new List<string> { "Admin" };


            var _userMgr = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);

            _userMgr.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _userMgr.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(roles);


            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
            var _signInMgr = new Mock<SignInManager<AppUser>>(_userMgr.Object,
                           _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null, null);

            _signInMgr.Setup(x => x.PasswordSignInAsync(user, pass, rem, false)).ReturnsAsync(SignInResult.Success);

            string jwtkey = "jmhbsfjkgnskdlrjgm";

            var _jwt = new Mock<IJwtService>();

            _jwt.Setup(x => x.GenerateToken(user, roles)).Returns(jwtkey);


            Service = new AuthService(_userMgr.Object, _signInMgr.Object, _jwt.Object);


        }

        [Fact]
        public async Task ValidLogInTest()
        {
            
          
            //Act
            var response = await Service.Login(email,pass,rem);

            //Assert
            Assert.True(response.status);

        }


        [Theory]
        [InlineData("email", pass, rem)]
        [InlineData(email, "ghtu", rem)]
        public async Task InvalidLogInTest(string email, string password, bool rememberMe)
        {


            //Act
            var response = await Service.Login(email, password, rememberMe);

            //Assert
            Assert.False(response.status);

        }





    }

}
