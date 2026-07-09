using AutoMapper;
using Domain.Entities;
using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapping
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<ApplicationUser, User>().ReverseMap();
        }
    }
}
