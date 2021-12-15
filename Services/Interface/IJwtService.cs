using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobListing.Core.Services
{
  public interface IJwtService
    {
        //string GenerateToken(User user, List<string> userRoles);
        string GenerateToken(AppUser response, List<string> roles);
    }
}
