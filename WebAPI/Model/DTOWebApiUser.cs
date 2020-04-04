using Newtonsoft.Json;

namespace WebAPI.Model
{
    public class DTOWebApiUser
    {
        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
