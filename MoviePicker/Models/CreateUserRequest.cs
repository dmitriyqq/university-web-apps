using Newtonsoft.Json;

namespace MoviePicker.Models
{
    public class CreateUserRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}