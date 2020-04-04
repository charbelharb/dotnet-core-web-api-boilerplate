using Newtonsoft.Json;

namespace WebAPI.Model
{
    public class DTOWebApiResponse
    {
        [JsonProperty("Data")]
        public object Data { get; set; }
    }
}
