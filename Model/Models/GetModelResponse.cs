

using Newtonsoft.Json;
using System;

namespace Leonardo.Model.Models
{
    public class GetModelResponse
    {
        [JsonProperty("custom_models_by_pk")]
        public GetModel TrainedModel { get; set; }
    }
    public class GetModel
    {
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("instancePrompt")]
        public string InstancePrompt { get; set; }

        [JsonProperty("modelHeight")]
        public int ModelHeight { get; set; }

        [JsonProperty("modelWidth")]
        public int ModelWidth { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("sdVersion")]
        public string SdVersion { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }

}

