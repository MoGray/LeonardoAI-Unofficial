using Newtonsoft.Json;
using System.Collections.Generic;


namespace Leonardo.Generation.Models
{
    public class GeneratedImages
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("nsfw")]
        public bool Nsfw { get; set; }

        [JsonProperty("likeCount")]
        public int LikeCount { get; set; }

        [JsonProperty("generated_image_variation_generics")]
        public List<GeneratedImageVariationGenerics>? GeneratedImageVariationGenerics { get; set; }
    }
}
