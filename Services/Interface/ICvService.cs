using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Core.Interface
{
    public interface ICvService
    {

        public Task<Tuple<bool, UploadCvDto>> UploadCvAsync(UploadCvDto model, string userId);
        public Task<Tuple <bool, UploadCvDto>> AddCvAsync(UploadCvDto model, string userId);
        public Task<List<Cv>> GetUserCvAsync(string userId);
        public Task<bool> DeleteCvAsync(string PublicId);


    }
}
