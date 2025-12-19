using AutoMapper;
using IFS.Person.WebApi.Domain.Models;
using IFS.Person.WebApi.Domain.Services.Interfaces;
using IFS.Person.WebApi.Repository.PersonRepository.Interfaces;

namespace IFS.Person.WebApi.Domain.Services;

public class PersonService : IPersonService
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _personRepository;

    public PersonService(IMapper mapper, IPersonRepository personRepository)
    {
        _mapper = mapper;
        _personRepository = personRepository;
    }

    public async Task<List<ActiveUserModel>> GetActiveUsersAsync(string active)
    {
        var response = await _personRepository.GetActiveUsersAsync(active);
        return _mapper.Map<List<ActiveUserModel>>(response);
    }

    public async Task<PersonGeneralInfoModel> GetPersonGeneralInfoAsync(string userId)
    {
        var response = await _personRepository.GetPersonGeneralInfoAsync(userId);
        return _mapper.Map<PersonGeneralInfoModel>(response);
    }    
    
    public async Task<List<PersonCompanyInfoModel>> GetPersonCompanyInfoAsync(string personId)
    {
        var response = await _personRepository.GetPersonCompanyInfoAsync(personId);
        return _mapper.Map<List<PersonCompanyInfoModel>>(response);
    }

    public async Task<List<PersonGeneralInfoModel>> GetPersonGeneralInfoAsync()
    {
        var response = await _personRepository.GetPersonGeneralInfoAsync();
        return _mapper.Map<List<PersonGeneralInfoModel>>(response);
    }
}