using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Leonardo.Generation.Models
{
    public class Generations
    {
        [JsonProperty("generated_images")]
        public List<GeneratedImages>? GeneratedImages { get; set; }

        [JsonProperty("modelId")]
        public string ModelId { get; set; }

        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("negativePrompt")]
        public string? NegativePrompt { get; set; }

        [JsonProperty("imageHeight")]
        public int ImageHeight { get; set; }

        [JsonProperty("imageWidth")]
        public int ImageWidth { get; set; }

        [JsonProperty("inferenceSteps")]
        public int InferenceSteps { get; set; }

        //This can be null if the images are not yet generated.
        [JsonProperty("seed")]
        public long? Seed { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("scheduler")]
        public string? Scheduler { get; set; }

        [JsonProperty("sdVersion")]
        public string? SdVersion { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("presetStyle")]
        public string? PresetStyle { get; set; }

        [JsonProperty("initStrength")]
        public string? InitStrength { get; set; }

        [JsonProperty("guidanceScale")]
        public int GuidanceScale { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }
    }
}
