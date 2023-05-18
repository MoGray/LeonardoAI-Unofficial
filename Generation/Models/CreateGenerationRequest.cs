

using Newtonsoft.Json;
using NSpecifications;
using System;

namespace Leonardo.Generation.Models
{
    public sealed class CreateGenerationRequest
    {
        public CreateGenerationRequest(string prompt
            , string? negativePrompt = null
            , string? modelId = "291be633-cb24-434f-898f-e662799936ad"
            , string? sdVersion = null
            , int numImages = 4
            , int width = 512
            , int height = 512
            , int numInferenceSteps = 0
            , int guidanceScale = 7
            , string? initGenerationImageId = null
            , string? initImageId = null
            , float? initStrength = null
            , string? scheduler = null
            , string? presetStyle = null
            , bool? tiling = null
            , bool? isPublic = null
            , bool? promptMagic = null
            , bool? controlNet = null
            , string? controlNetType = null)
        {
            Prompt = prompt;
            NegativePrompt = negativePrompt;
            ModelId = modelId;
            SdVersion = sdVersion;
            NumImages = numImages;
            Width = width;
            Height = height;
            NumInferenceSteps = numInferenceSteps;
            GuidanceScale = guidanceScale;
            InitGenerationImageId = initGenerationImageId;
            InitImageId = initImageId;
            InitStrength = initStrength;
            Scheduler = scheduler;
            PresetStyle = presetStyle;
            Tiling = tiling;
            Public = isPublic;
            PromptMagic = promptMagic;
            ControlNet = controlNet;
            ControlNetType = controlNetType;
        }

        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("negative_prompt")]
        public string? NegativePrompt { get; set; }

        [JsonProperty("modelId")]
        public string? ModelId { get; set; } /*The model ID used for the image generation. If not provided uses sd_version to determine the version of Stable Diffusion to use.

                                                Leonardo Creative: 6bef9f1b-29cb-40c7-b9df-32b51c1f67d3
                                                Leonardo Select: cd2b2a15-9760-4174-a5ff-4d2925057376
                                                Leonardo Signature: 291be633-cb24-434f-898f-e662799936ad
        */

        [JsonProperty("sd_version")]
        public string? SdVersion { get; set; } //The base version of stable diffusion to use if not using a custom model. v1_5 is 1.5, v2 is 2.1, if not specified it will default to v1_5

        [JsonProperty("num_images")]
        public int NumImages { get; set; } //Must be between 1 and 8. If either width or height is over 768, must be between 1 and 4

        [JsonProperty("width")]
        public int Width { get; set; } //Must be between 32 and 1024 and be a multiple of 8

        [JsonProperty("height")]
        public int Height { get; set; } //Must be between 32 and 1024 and be a multiple of 8

        [JsonProperty("num_inference_steps")]
        public int NumInferenceSteps { get; set; } //The number of inference steps to use for the generation. Must be between 30 and 60.

        [JsonProperty("guidance_scale")]
        public int GuidanceScale { get; set; } //How strongly the generation should reflect the prompt. 7 is recommended. Must be between 1 and 20.

        [JsonProperty("init_generation_image_id")]
        public string? InitGenerationImageId { get; set; } //The ID of an existing image to use in image2image.

        [JsonProperty("init_image_id")]
        public string? InitImageId { get; set; } //The ID of an Init Image to use in image2image.

        [JsonProperty("init_strength")]
        public float? InitStrength { get; set; } //How strongly the generated images should reflect the original image in image2image. Must be a float between 0.1 and 0.9.

        [JsonProperty("scheduler")]
        public string? Scheduler { get; set; } //The scheduler to generate images with. Defaults to EULER_DISCRETE if not specified.

        [JsonProperty("presetStyle")]
        public string? PresetStyle { get; set; } //The style to generate images with.

        [JsonProperty("tiling")]
        public bool? Tiling { get; set; } //Whether the generated images should tile on all axis.

        [JsonProperty("public")]
        public bool? Public { get; set; } //Whether the generated images should show in the community feed.

        [JsonProperty("promptMagic")]
        public bool? PromptMagic { get; set; } //Enable to use Prompt Magic.

        [JsonProperty("controlNet")]
        public bool? ControlNet { get; set; } //Enable to use ControlNet. Requires an init image to be provided. Requires a model based on SD v1.5

        [JsonProperty("controlNetType")]
        public string? ControlNetType { get; set; } //The type of ControlNet to use.

        public bool Validate()
        {
            var heightSpec = new Spec<CreateGenerationRequest>(gen => gen.Height >= 32 && gen.Height <= 1024 && gen.Height % 8 == 0);
            var widthSpec = new Spec<CreateGenerationRequest>(gen => gen.Width >= 32 && gen.Width <= 1024 && gen.Width % 8 == 0);
            var numImagesSpec = new Spec<CreateGenerationRequest>(gen => gen.NumImages >= 1 && gen.NumImages <= 8);
            var imageNumberAndSizeSpec = new Spec<CreateGenerationRequest>(gen => (gen.NumImages > 4 && gen.Height <= 768 && gen.Width <= 768) || (gen.NumImages <= 4 && gen.Height <= 1024 && gen.Width <= 1024));
            var initStrengthSpec = new Spec<CreateGenerationRequest>(gen => gen.InitStrength == null || (gen.InitStrength >= 0.1f && gen.InitStrength <= 0.9f));
            var controlNetSpec = new Spec<CreateGenerationRequest>(gen => gen.ControlNet == null || gen.ControlNet == false || (gen.ControlNet == true && gen.InitImageId != null));
            var promptSpec = new Spec<CreateGenerationRequest>(gen => gen.Prompt.Length <= 1000 );
            var negativePromptSpec = new Spec<CreateGenerationRequest>(gen => gen.NegativePrompt == null || gen.NegativePrompt.Length <= 1000);

            if (!heightSpec.IsSatisfiedBy(this))
                throw new ArgumentOutOfRangeException("The height is either less than 32px, greater than 1024px or an image height not divisible by 8");
            if (!widthSpec.IsSatisfiedBy(this))
                throw new ArgumentOutOfRangeException("The width is either less than 32px, greater than 1024px or an image width not divisible by 8");
            if (!numImagesSpec.IsSatisfiedBy(this))
                throw new ArgumentOutOfRangeException("You must select the amount of images to run between 1 image to 8 images");
            if (!imageNumberAndSizeSpec.IsSatisfiedBy(this))
                throw new ArgumentOutOfRangeException("If you are running more than 4 images the height and width maximum size is 768px");
            if (!initStrengthSpec.IsSatisfiedBy(this))
                throw new ArgumentOutOfRangeException("If you supply an init strength it must be a float between 0.1 and 0.9");
            if (!controlNetSpec.IsSatisfiedBy(this))
                throw new ArgumentException("If you select to use ControlNet you must submit an initialized image");
            if (!promptSpec.IsSatisfiedBy(this))
                throw new ArgumentException("Prompt must be less than or equal to 1000 characters");
            if (!negativePromptSpec.IsSatisfiedBy(this))
                throw new ArgumentException("NegativePrompt must be less than or equal to 1000 characters");

            return true;
        }
    }
}
