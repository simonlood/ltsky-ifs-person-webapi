using IFS.Person.WebApi.Domain.Models;

namespace IFS.Person.WebApi.Domain.Services.Interfaces;

public interface IPersonService
{
    Task<List<ActiveUserModel>> GetActiveUsersAsync(string active);
    Task<PersonGeneralInfoModel> GetPersonGeneralInfoAsync(string userId);
    Task<List<PersonCompanyInfoModel>> GetPersonCompanyInfoAsync(string personId);
    Task<List<PersonGeneralInfoModel>> GetPersonGeneralInfoAsync();
}