

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Leonardo.Variation.Models
{
    public class GetVariationResponse
    {
        [JsonProperty("generated_image_variation_generic")]
        public List<VariationImage> Images { get; set; }
    }

    public class VariationImage
    {
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("transformType")]
        public string TransformType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
