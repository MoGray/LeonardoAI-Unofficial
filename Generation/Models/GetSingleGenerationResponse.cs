

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Leonardo.Generation.Models
{
    public class GetSingleGenerationResponse
    {
        [JsonProperty("generations_by_pk")]
        public Generations Generations { get; set; }
    }
}
