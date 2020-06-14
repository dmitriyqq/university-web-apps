using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MoviePicker.Models
{
    public class Movie : Model
    {
        [BsonElement("imdbId")]
        [JsonProperty("imdbId")]
        public string ImdbId { get; set; }

        [BsonElement("title")]
        [JsonProperty("title")]
        public string Title { get; set; }

        [BsonElement("year")]
        [JsonProperty("year")]
        public string Year { get; set; }

        [BsonElement("director")]
        [JsonProperty("director")]
        public string Director { get; set; }

        [BsonElement("runtime")]
        [JsonProperty("runtime")]
        public string Runtime { get; set; }

        [BsonElement("country")]
        [JsonProperty("country")]
        public string Country { get; set; }

        [BsonElement("poster")]
        [JsonProperty("poster")]
        public string Poster { get; set; }

        [BsonElement("imdbRating")]
        [JsonProperty("imdbRating")]
        public string ImdbRating { get; set; }

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

        [BsonElement("dateWatched")]
        [JsonProperty("dateWatched")]
        public DateTime TimeWatched { get; set; }
    }
}
