using JobListing.Data.EFCore.Interface;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.EFCore.Implementation
{
    public class CvRepository : ICvRepository

    { 
        private readonly JobListingContext _ctx;

        public CvRepository(JobListingContext ctx)
    {
            _ctx = ctx;
    }

    
        public async Task<bool> Add<T>(T entity, string role = "role")
        {
            _ctx.Add(entity);
            return await SaveChanges();
        }

        public async Task<bool> Delete<T>(T entity)
        {
            _ctx.Remove(entity);
            return await SaveChanges();
        }

        public async Task <bool> Edit<T>(T entity)
        {
            _ctx.Update(entity);
            return await SaveChanges();
        }

        public async Task<Cv> GetCvByPublicId(string PublicId)
        {
            return await _ctx.Cvs.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.PublicId == PublicId);
        }

        public async Task<List<Cv>> GetCvByUserId(string UserId)
        {
            return await _ctx.Cvs.Where(x => x.AppUserId == UserId).ToListAsync();
        }

        public async Task<List<Cv>> GetCvs()
        {
            return await _ctx.Cvs.ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
