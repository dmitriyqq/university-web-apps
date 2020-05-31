using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MoviePicker.Models
{
    public class CollectionHistoryItem : Model
    {
        [BsonElement("type")]
        [JsonProperty("type")]
        public CollectionHistoryItemType Type { get; set; }

        [BsonElement("user")]
        [JsonProperty("user")]
        public User User { get; set; }

        [BsonElement("collection")]
        [JsonProperty("collection")]
        public MovieCollection Collection { get; set; }

        [BsonElement("movie")]
        [JsonProperty("movie")]
        public Movie Movie { get; set; }
    }
}
