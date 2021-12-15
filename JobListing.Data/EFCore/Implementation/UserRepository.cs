using JobListing.Data.EFCore.Interface;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.EFCore.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly UserManager<AppUser> userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Add<T>(T entity, string role)
        {

            var user = entity as AppUser;
            var add = await userManager.CreateAsync(user);
            if (add.Succeeded)
            {
               await userManager.AddToRoleAsync(user, role);
                return true;
            }

            return false;
        }

        public async Task<bool> Delete<T>(T entity)
        {
            var email = entity as string;
            var user = await GetUserByEmail(email);
            var delete = await userManager.DeleteAsync(user);
            if (delete.Succeeded)
            {
                return true;
            }
           return false;   
        }

        public async Task<bool> Edit<T>(T entity)
        {
            var user = entity as AppUser;
            var edit = await userManager.UpdateAsync(user);
            if (edit.Succeeded)
            {
                return true;
            }
                 return false;
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            var res =await userManager.FindByEmailAsync(email);
            return res;
        }

        public async Task<AppUser> GetUserById(string id)
        {

            var res = await userManager.FindByIdAsync(id);
            return res;
        }

        public  Task<List<AppUser>> GetUsers()
        {
            var res =  userManager.Users;
            List<AppUser> result = new List<AppUser>();
            foreach(var user in res)
            {
                result.Add(user);
            }
            return Task.FromResult(result);
        }
    }
}
