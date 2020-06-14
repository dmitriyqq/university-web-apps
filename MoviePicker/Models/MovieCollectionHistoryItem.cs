using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MoviePicker.Models
{
    public class MovieCollectionHistoryItem : Model
    {
        [BsonElement("type")]
        [JsonProperty("type")]
        public MovieCollectionHistoryItemType Type { get; set; }

        [BsonElement("user")]
        [JsonProperty("user")]
        public User User { get; set; }

        [BsonElement("collectionId")]
        [JsonProperty("collectionId")]
        public string CollectionId { get; set; }

        [BsonElement("movie")]
        [JsonProperty("movie")]
        public Movie Movie { get; set; }


        [BsonElement("time")]
        [JsonProperty("time")]
        public DateTime Time { get; set; }
    }
}
