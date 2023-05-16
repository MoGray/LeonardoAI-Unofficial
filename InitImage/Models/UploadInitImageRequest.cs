using Newtonsoft.Json;

namespace Leonardo.InitImage.Models
{
    internal class UploadInitImageRequest
    {
        public UploadInitImageRequest(string extension)
        {
            Extension = extension;
        }

        [JsonProperty("extension")]
        public string Extension { get; set; }
    }
}
