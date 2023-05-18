

using Newtonsoft.Json;
using System;

namespace Leonardo.InitImage.Models
{
    public class GetInitImageResponse
    {
        [JsonProperty("init_images_by_pk")]
        public GetInitImage GetInitImage { get; set; }
    }

    public class GetInitImage
    {
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
