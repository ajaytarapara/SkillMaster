using AutoMapper;
using CIPLATFORM_SKILL_PM.Models.Data;
using CIPLATFORM_SKILL_PM.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Models.MapperHelper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, LoginModel>().ReverseMap();
            CreateMap<User, SessionDetailsViewModel>()
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")); 
        }
    }
}
