
using Newtonsoft.Json;

namespace Leonardo.Variation.Models
{
    public class CreateUpscaleRequest
    {
        public CreateUpscaleRequest(string id)
        {
            Id = id;
        }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
