using Newtonsoft.Json;

namespace IFS.Person.WebApi.Repository.PersonRepository.Models
{
    public class PersonCompanyInfoResponse
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { set; get; }
        public List<PersonCompanyInfo> Value { get; set; }
    }
}
