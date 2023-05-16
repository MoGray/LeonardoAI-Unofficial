
using Newtonsoft.Json;

namespace Leonardo.InitImage.Models
{
    public class UploadInitImageResponse
    {
        [JsonProperty("uploadInitImage")]
        public UploadInitImage UploadInitImage { get; set; }
    }

    public class UploadInitImage
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("fields")]
        public string Fields { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("_typename")]
        public string TypeName { get; set; }
    }
}
