

using Newtonsoft.Json;

namespace Leonardo.Generation.Models
{
    public class CreateGenerationResponse
    {
        [JsonProperty("sdGenerationJob")]
        public SdGenerationJob SdGenerationJob { get; set; }
    }

    public class SdGenerationJob
    {
        [JsonProperty("generationId")]
        public string GenerationId { get; set; }
    }
}
