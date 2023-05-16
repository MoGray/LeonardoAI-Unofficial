

using Newtonsoft.Json;

namespace Leonardo.Generation.Models
{
    public class GeneratedImageVariationGenerics
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("transformType")]
        public string TransformType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
