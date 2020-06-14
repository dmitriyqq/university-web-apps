using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviePicker {
    public class OMDBMovie
    {

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("Runtime")]
        public string Runtime { get; set; }

        [JsonProperty("Director")]
        public string Director { get; set; }

        [JsonProperty("Poster")]
        public string Poster { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("ImdbRating")]
        public string ImdbRating { get; set; }
        
        [JsonProperty("Response")]
        public string Response { get; set; }

        [JsonProperty("imdbID")]
        public string ImdbId { get; set; }
    }

    public class OMDBSearchMovie
    {

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("Poster")]
        public string Poster { get; set; }

        [JsonProperty("imdbID")]
        public string ImdbId { get; set; }
    }        

    public class OMDBSearchResponse 
    {
        [JsonProperty("Response")]
        public string Response { get; set; }

        [JsonProperty("totalResults")]
        public string TotalResults { get; set; } 

        [JsonProperty("Search")]
        public IEnumerable<OMDBSearchMovie> Search { get; set; }
    }
}