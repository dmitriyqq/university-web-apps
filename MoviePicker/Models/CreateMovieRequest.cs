using Newtonsoft.Json;

namespace MoviePicker.Models
{
    public class CreateMovieRequest
    {
        [JsonProperty("imdbId")]
        public string ImdbId { get; set; }
    }
}