

using Newtonsoft.Json;

namespace Leonardo.Dataset.Models
{
    public class UploadDatasetImageResponse
    {
        [JsonProperty("uploadDatasetImage")]
        public UploadDatasetImage Image { get; set; }
    }

    public class UploadDatasetImage
    {
        [JsonProperty("fields")]
        public string Fields { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
