using Newtonsoft.Json;

namespace IFS.Person.WebApi.Repository.PersonRepository.Models
{
    public class PersonGeneralInfoResponse
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { set; get; }
        public List<PersonGeneralInfo> Value { get; set; }
    }
}
