

using Newtonsoft.Json;

namespace Leonardo.Generation.Models
{
    public class GetSingleGenerationRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
