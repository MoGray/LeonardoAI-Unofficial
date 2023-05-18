

using Newtonsoft.Json;

namespace Leonardo.Dataset.Models
{
    public class CreateDatasetResponse
    {
        [JsonProperty("insert_datasets_one")]
        public CreateDataset CreateDataset { get; set; }
    }

    public class CreateDataset
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
