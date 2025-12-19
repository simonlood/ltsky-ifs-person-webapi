using Newtonsoft.Json;

namespace IFS.Person.WebApi.Repository.PersonRepository.Models
{
    public class ActiveUserResponse
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { set; get; }
        public List<ActiveUser> Value { get; set; }
    }
}
