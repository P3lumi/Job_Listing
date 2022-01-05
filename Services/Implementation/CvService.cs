using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using JobListing.Common.Helper;
using JobListing.Core.Interface;
using JobListing.Data.EFCore.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Core.Implementation
{
    public class CvService : ICvService
    {

        private readonly Cloudinary _cloudinary;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userMgr;
        private readonly ICvRepository _cvRepo;

         
        public CvService(
            IOptions<CloudinarySettings> config, 
            IMapper mapper, UserManager<AppUser> userManager, ICvRepository cvRepository)
        {
            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
            _mapper = mapper;
            _userMgr = userManager;
            _cvRepo = cvRepository;
        }



        public async Task<Tuple<bool, UploadCvDto>> AddCvAsync(UploadCvDto model, string userId)
        {
            var user = await _userMgr.Users.Include(x => x.Cvs).FirstOrDefaultAsync(x => x.Id == userId);
            //var cv = _mapper.Map<Cv>(model);
            var cv = new Cv {
                PublicId = model.PublicId,
                Url = model.Url
            };
            cv.AppUserId = userId;
            var res = await _cvRepo.Add(cv);

            return new Tuple<bool, UploadCvDto>(res, model);

        }

        public async Task<bool> DeleteCvAsync(string userId)
        {
            DeletionParams destroyParams = new DeletionParams(userId)
            {
                ResourceType = ResourceType.Image
            };

            DeletionResult destroyResult = _cloudinary.Destroy(destroyParams);

            if (destroyResult.StatusCode.ToString().Equals("OK"))
            {
                var cv = await _cvRepo.GetCvByPublicId(userId);
                if(cv != null)
                {
                    var res = await _cvRepo.Delete(cv);
                    if (res)
                        return true;
                }
            }
            return false;

        }

        public async Task<List<Cv>> GetUserCvAsync(string userId)
        {

            var res = await _cvRepo.GetCvByUserId(userId);
            
            if (res != null)
                return res;

            return null;

        }

        public async  Task<Tuple<bool, UploadCvDto>> UploadCvAsync(UploadCvDto model, string userId)
        {

            var uploadResult = new RawUploadResult();

            using (var stream = model.Cv.OpenReadStream())
            {
                var rawUploadParams = new RawUploadParams
                {
                    File = new FileDescription(model.Cv.FileName, stream)
                };

                uploadResult = await _cloudinary.UploadAsync(rawUploadParams);
            }

            var status = uploadResult.StatusCode.ToString();

            if (status.Equals("OK"))
            {
                model.PublicId = uploadResult.PublicId;
                model.Url = uploadResult.Url.ToString();
                return new Tuple<bool, UploadCvDto>(true, model);
            }

            return new Tuple<bool, UploadCvDto>(false, model);

        }
    }
}
