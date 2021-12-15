﻿using Microsoft.AspNetCore.Identity;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.EFCore
{
    public class SeederClass
    {
        private readonly JobListingContext _ctx;
        private UserManager<AppUser> _userMgr;
        private RoleManager<IdentityRole> _roleMgr;

        public SeederClass(JobListingContext ctx, UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _userMgr = userManager;
            _roleMgr = roleManager;

        }

        public async Task SeedMe()
        {
            _ctx.Database.EnsureCreated();

            try
            {
                var roles = new string[] { "Applicant", "Admin" };
                if (!_roleMgr.Roles.Any())
                {
                    foreach (var role in roles)
                    {
                        await _roleMgr.CreateAsync(new IdentityRole(role));
                    }
                }
                var CurrentDirectory = Environment.CurrentDirectory;

                var data = System.IO.File.ReadAllText(@"C:\Users\hp\source\repos\JobListing\JobListing.Data\EFCore\SeedData.json");
                var ListOfAppUsers = JsonConvert.DeserializeObject<List<AppUser>>(data);


                if (!_userMgr.Users.Any())
                {
                    var counter = 0;
                    var role = "";
                    foreach (var user in ListOfAppUsers)
                    {
                        user.UserName = user.Email;
                        role = counter < 1 ? roles[1] : roles[0];

                        var res = await _userMgr.CreateAsync(user, "P@55w0rd");
                        if (res.Succeeded)
                            await _userMgr.AddToRoleAsync(user, role);

                        counter++;

                    }
                }

            } 
            catch (DbException dbex)
            {
                throw new Exception(dbex.Message);
            }



        }

        }
}
