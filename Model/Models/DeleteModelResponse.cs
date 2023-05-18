

using Newtonsoft.Json;

namespace Leonardo.Model.Models
{
    public class DeleteModelResponse
    {
        [JsonProperty("delete_custom_models_by_pk")]
        public DeletedModel DeletedModel { get; set; }
    }

    public class DeletedModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
