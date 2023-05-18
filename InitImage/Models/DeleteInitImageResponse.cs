

using Newtonsoft.Json;

namespace Leonardo.InitImage.Models
{
    public class DeleteInitImageResponse
    {
        [JsonProperty("delete_init_images_by_pk")]
        public DeleteInitImage DeleteInitImage { get; set; }
    }

    public class DeleteInitImage
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
