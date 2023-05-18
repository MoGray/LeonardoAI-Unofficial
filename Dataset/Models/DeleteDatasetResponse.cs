

using Newtonsoft.Json;

namespace Leonardo.Dataset.Models
{
    public class DeleteDatasetResponse
    {
        [JsonProperty("delete_datasets_by_pk")]
        public DeletedDataset DeletedDataset { get; set; }
    }

    public class DeletedDataset
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
