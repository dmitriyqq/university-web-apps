using Newtonsoft.Json;

namespace MoviePicker.Models
{
    public class CreateMovieCollectionRequest
    {
        [JsonProperty("name")]
        public string CollectionName { get; set; }
    }
}