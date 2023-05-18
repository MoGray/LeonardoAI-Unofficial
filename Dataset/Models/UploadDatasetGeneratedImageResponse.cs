using Newtonsoft.Json;

namespace Leonardo.Dataset.Models
{
    public class UploadDatasetGeneratedImageResponse
    {
        [JsonProperty("uploadDatasetImageFromGen")]
        public UploadGeneratedDatasetImage Image { get; set; }
    }
    public class UploadGeneratedDatasetImage
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
