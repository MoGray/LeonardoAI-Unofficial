
using Newtonsoft.Json;

namespace Leonardo.Dataset.Models
{
    public class UploadDatasetGeneratedImageRequest
    {
        public UploadDatasetGeneratedImageRequest(string imageId)
        {
            ImageId = imageId;
        }

        [JsonProperty("generatedImageId")]
        public string ImageId { get; set; }
    }
}
