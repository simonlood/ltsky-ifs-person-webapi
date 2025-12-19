using IFS.Person.WebApi.Infrastructure.Managers.Interfaces;
using IFS.Person.WebApi.Repository.Exceptions;
using IFS.Person.WebApi.Repository.PersonRepository.Interfaces;
using IFS.Person.WebApi.Repository.PersonRepository.Models;

namespace IFS.Person.WebApi.Repository.PersonRepository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IHttpClientManager _clientManager;
        private readonly string IFSHttpClient = "IFSHttpClient";

        public PersonRepository(IHttpClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        public async Task<List<ActiveUser>> GetActiveUsersAsync(string active)
        {
            var httpClient = _clientManager.GetHttpClient(IFSHttpClient);

            var result = await httpClient.GetAsync($"UserHandling.svc/Users?$filter=Active eq {active} and not startswith(Identity, 'B2B') and not startswith(Identity, 'X') and not startswith(Description, 'IFS') ");
            var stringResult = result.Content.ReadAsStringAsync().Result;

            if (result.IsSuccessStatusCode)
            {
                var personResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ActiveUserResponse>(stringResult);
                return personResponse.Value;
            }

            throw new IfsCustomException(nameof(GetActiveUsersAsync) + stringResult);
        }

        public async Task<PersonGeneralInfo> GetPersonGeneralInfoAsync(string userId)
        {
            var httpClient = _clientManager.GetHttpClient(IFSHttpClient);

            var result = await httpClient.GetAsync($"PersonHandling.svc/PersonInfoSet?$filter=UserId eq '{userId}'");
            var stringResult = result.Content.ReadAsStringAsync().Result;

            if (result.IsSuccessStatusCode)
            {
                var personResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<PersonGeneralInfoResponse>(stringResult);
                return personResponse.Value.FirstOrDefault();
            }

            throw new IfsCustomException(nameof(GetPersonGeneralInfoAsync) + stringResult);
        }        
        
        public async Task<List<PersonCompanyInfo>> GetPersonCompanyInfoAsync(string personId)
        {
            var httpClient = _clientManager.GetHttpClient(IFSHttpClient);

            var result = await httpClient.GetAsync($"PersonnelFileHandling.svc/CompanyPersonSet?$filter=PersonId eq '{personId}'");
            var stringResult = result.Content.ReadAsStringAsync().Result;

            if (result.IsSuccessStatusCode)
            {
                var personResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<PersonCompanyInfoResponse>(stringResult);
                return personResponse.Value;
            }

            throw new IfsCustomException(nameof(GetPersonCompanyInfoAsync) + stringResult);
        }

        public async Task<List<PersonGeneralInfo>> GetPersonGeneralInfoAsync()
        {
            var httpClient = _clientManager.GetHttpClient(IFSHttpClient);

            var result = await httpClient.GetAsync($"PersonHandling.svc/PersonInfoSet");
            var stringResult = result.Content.ReadAsStringAsync().Result;

            if (result.IsSuccessStatusCode)
            {
                var personResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<PersonGeneralInfoResponse>(stringResult);
                return personResponse.Value;
            }

            throw new IfsCustomException(nameof(GetPersonGeneralInfoAsync) + stringResult);
        }
    }
}
