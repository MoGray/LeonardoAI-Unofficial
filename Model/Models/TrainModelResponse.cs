

using Newtonsoft.Json;

namespace Leonardo.Model.Models
{
    public class TrainModelResponse
    {
        [JsonProperty("sdTrainingJob")]
        public TrainedModel TrainedModel { get; set; }
    }

    public class TrainedModel
    {
        [JsonProperty("customModelId")]
        public string Id { get; set; }
    }
}
