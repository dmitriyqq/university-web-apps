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
        public string Owner { get; set; }

        [BsonElement("groupMembers")]
        [JsonProperty("groupMembers")]
        public IEnumerable<User> GroupMemebers { get; set; }
    }
}
