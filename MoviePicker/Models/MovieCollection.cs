using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MoviePicker.Models
{
    public class MovieCollection : Model
    {
        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [BsonElement("owner")]
        [JsonProperty("owner")]
        public User Owner { get; set; }

        [BsonElement("selectedMovie")]
        [JsonProperty("selectedMovie")]
        public string SelectedMovie { get; set; }

        [BsonElement("groupMembers")]
        [JsonProperty("groupMembers")]
        public IEnumerable<User> GroupMemebers { get; set; }

        [BsonIgnore]
        [JsonProperty("history", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<MovieCollectionHistoryItem> History { get; set; }

        [BsonIgnore]
        [JsonProperty("movies", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Movie> Movies { get; set; }

    }
}
