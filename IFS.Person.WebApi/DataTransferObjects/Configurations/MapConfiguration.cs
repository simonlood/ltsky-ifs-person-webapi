using AutoMapper;
using IFS.Person.WebApi.Domain.Models;

namespace IFS.Person.WebApi.DataTransferObjects.Configurations;

public class MapConfiguration : Profile
{
    public MapConfiguration()
    {
        CreateMap<ActiveUserModel, ActiveUserDto>();
        CreateMap<Repository.PersonRepository.Models.ActiveUser, ActiveUserModel>();

        CreateMap<PersonGeneralInfoModel, PersonGeneralInfoDto>();            
        CreateMap<Repository.PersonRepository.Models.PersonGeneralInfo, PersonGeneralInfoModel>();        
        
        CreateMap<PersonCompanyInfoModel, PersonCompanyInfoDto>();
        CreateMap<Repository.PersonRepository.Models.PersonCompanyInfo, PersonCompanyInfoModel>();
    }
}