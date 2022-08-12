using Newtonsoft.Json;

namespace WebAPI.Model
{
    public class DtoWebApiResponse
    {
        [JsonProperty("Data")]
        public object? Data { get; set; }
    }
}
