
using Newtonsoft.Json;

namespace Leonardo.Dataset.Models
{
    public class UploadDatasetImageRequest
    {
        public UploadDatasetImageRequest(string extension)
        {
            Extension = extension;
        }

        [JsonProperty("extension")]
        public string Extension { get; set; }
    }
}
