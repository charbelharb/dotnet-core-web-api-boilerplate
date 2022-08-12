using Newtonsoft.Json;

namespace WebAPI.Model
{
    public class DtoWebApiUser
    {
        [JsonProperty("Username")]
        public string? Username { get; set; }

        [JsonProperty("Password")]
        public string? Password { get; set; }
    }
}
