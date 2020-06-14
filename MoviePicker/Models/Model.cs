using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MoviePicker.Models
{
    public class Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
