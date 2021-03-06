using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Core.Interface
{
    public interface IAuthService
    {
        Task<LogInCredDto> Login(string email, string password, bool rememberMe);
    }
}
