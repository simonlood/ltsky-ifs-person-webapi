using IFS.Person.WebApi.Repository.PersonRepository.Models;

namespace IFS.Person.WebApi.Repository.PersonRepository.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<ActiveUser>> GetActiveUsersAsync(string active);
        Task<PersonGeneralInfo> GetPersonGeneralInfoAsync(string userId);
        Task<List<PersonCompanyInfo>> GetPersonCompanyInfoAsync(string personId);
        Task<List<PersonGeneralInfo>> GetPersonGeneralInfoAsync();
    }
}
