using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MoviePicker.Models
{
    public class Movie: Model
    {
        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [BsonElement("year")]
        [JsonProperty("year")]
        public string Year { get; set; }

        [BsonElement("director")]
        [JsonProperty("director")]
        public string Director { get; set; }

        [BsonElement("link")]
        [JsonProperty("link")]
        public string Link { get; set; }

        [BsonElement("addedBy")]
        [JsonProperty("addedBy")]
        public User AddedBy { get; set; }

        [BsonElement("collectionId")]
        [JsonProperty("collectionId")]
        public string CollectionId { get; set; }

        [BsonElement("watched")]
        [JsonProperty("watched")]
        public bool Watched { get; set; }
    }
}
