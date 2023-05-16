

using Newtonsoft.Json;

namespace Leonardo.Generation.Models
{
    public class GenerationDeletionResponse
    {
        [JsonProperty("delete_generations_by_pk")]
        public DeletionGenerationsByPk SdGenerationJob { get; set; }
    }

    public class DeletionGenerationsByPk
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
