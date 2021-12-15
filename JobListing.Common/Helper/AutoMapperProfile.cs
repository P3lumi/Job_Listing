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
            CreateMap<Job, JobToReturnDto>();
            CreateMap<Address, AddressToReturnDto>();

        }

    }
}
