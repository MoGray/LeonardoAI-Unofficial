

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Leonardo.Dataset.Models
{
    public class GetDatasetResponse
    {
        [JsonProperty("datasets_by_pk")]
        public GetDataset Dataset { get; set; }
    }

    public class GetDataset
    {
        [JsonProperty("dataset_images")]
        public List<DatasetImages> Images { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }

    public class DatasetImages
    {
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
