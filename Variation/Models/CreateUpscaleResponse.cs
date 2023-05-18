

using Newtonsoft.Json;

namespace Leonardo.Variation.Models
{
    public class CreateUpscaleResponse
    {
        [JsonProperty("sdUpscaleJob")]
        public UpscaleJob UpscaleJob { get; set; }
    }

    public class UpscaleJob
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
