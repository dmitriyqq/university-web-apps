using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MoviePicker.Models
{
    public class User : Model
    {
        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
