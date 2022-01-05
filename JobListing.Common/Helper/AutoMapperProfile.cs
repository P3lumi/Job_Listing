using AutoMapper;
using Models;
using Models.DTO;
using Models.JobDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Common.Helper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, UserToReturnDto>();
            CreateMap<JobDto, Job>();
            CreateMap<Job, JobToReturnDto>().ForMember(x=>x.industryName,y=>y.MapFrom(j=>j.Industry.IndustryName)).ForMember(x => x.categoryName, y => y.MapFrom(j => j.Category.Name));
            CreateMap<Address, AddressToReturnDto>();
            CreateMap<UploadCvDto, Cv>().ForMember(x => x.Url, y => y.MapFrom(j => j.Url)).ForMember(x => x.PublicId, y => y.MapFrom(j => j.PublicId));
           

        }

    }
}
