

using Newtonsoft.Json;

namespace Leonardo.Dataset.Models
{
    public class CreateDatasetRequest
    {
        public CreateDatasetRequest(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
