

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Leonardo.Generation.Models
{
    public class UserGenerations
    {
        [JsonProperty("generations")]
        public List<Generations> GeneratedI { get; set; }
    }
}
