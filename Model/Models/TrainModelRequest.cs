
using Newtonsoft.Json;
using NSpecifications;
using System;

namespace Leonardo.Model.Models
{
    public class TrainModelRequest
    {
        public TrainModelRequest(string name, string datasetId, string instancePrompt, string description = null, string modelType = "GENERAL", bool nsfw = false, int resolution = 512, string sdVersion = "v1_5", string strength = "MEDIUM")
        {
            Name = name;
            DatasetId = datasetId;
            InstancePrompt = instancePrompt;
            Description = description;
            ModelType = modelType;
            Nsfw = nsfw;
            Resolution = resolution;
            SdVersion = sdVersion;
            Strength = strength;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("datasetId")]
        public string? DatasetId { get; set; }

        [JsonProperty("instance_prompt")]
        public string? InstancePrompt { get; set; }

        [JsonProperty("modelType")]
        public string? ModelType { get; set; }

        [JsonProperty("nsfw")]
        public bool Nsfw { get; set; }

        [JsonProperty("resolution")]
        public int Resolution { get; set; }

        [JsonProperty("sd_Version")]
        public string? SdVersion { get; set; }

        [JsonProperty("strength")]
        public string? Strength { get; set; }

        public bool Validate()
        {
            var resolutionSpec = new Spec<TrainModelRequest>(train => train.Resolution >= 512 && train.Resolution <= 768 && train.Resolution % 8 == 0);
            var nameSpec = new Spec<TrainModelRequest>(train => !String.IsNullOrEmpty(train.Name));
            var datasetIdSpec = new Spec<TrainModelRequest>(train => !String.IsNullOrEmpty(train.DatasetId));
            var instancePrompt = new Spec<TrainModelRequest>(train => !String.IsNullOrEmpty(train.InstancePrompt));

            if (!resolutionSpec.IsSatisfiedBy(this))
                throw new ArgumentOutOfRangeException("The resolution is either less than 512px, greater than 768px or an not divisible by 8");
            if (!nameSpec.IsSatisfiedBy(this))
                throw new ArgumentOutOfRangeException("The name of the model must not be empty");
            if (!datasetIdSpec.IsSatisfiedBy(this))
                throw new ArgumentOutOfRangeException("Your datasetId must not be empty");
            if (!instancePrompt.IsSatisfiedBy(this))
                throw new ArgumentOutOfRangeException("Your instance prompt must not be empty");

            return true;
        }
    }
}
