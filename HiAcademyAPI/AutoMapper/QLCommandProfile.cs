using AutoMapper;
using HiAcademyAPI.ModelDTO;
using HiAcademyAPI.Models;
namespace HiAcademyAPI.AutoMapper
{
    public class QLCommandProfile : Profile
    {
        public QLCommandProfile()
        {
            CreateMap<AppCourseforuserDTO, AppCourseforuser>()
                .ForMember(x => x.IdcourseNavigation, opt => opt.Ignore())
                .ForMember(x => x.IduserNavigation, opt => opt.Ignore());
            CreateMap<AppCourseforuser, AppCourseforuserDTO>();

            CreateMap<AppCourseinfoDTO, AppCourseinfo>()
                .ForMember(x => x.IdcourseNavigation, opt => opt.Ignore())
                .ForMember(x => x.IdlessionNavigation, opt => opt.Ignore());
            CreateMap<AppCourseinfo, AppCourseinfoDTO>();

        }
    }
}
