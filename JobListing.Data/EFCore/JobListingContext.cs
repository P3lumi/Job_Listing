using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.EFCore
{
   public class JobListingContext: IdentityDbContext<AppUser>
    {
        public JobListingContext(DbContextOptions<JobListingContext> options): base(options)
        {
                
        }

       
        public DbSet<Address> Address { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Industry> Industry { get; set; }


    }


}
