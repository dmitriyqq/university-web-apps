using System;
using Newtonsoft.Json;

namespace MoviePicker {
    public class Paging {

        [JsonProperty("total")]
        public long TotalCount { get; set; }

        [JsonProperty("skip")]
        public int Skip { get; set; }

        [JsonProperty("take")]
        public int Take { get; set; }

        [JsonProperty("availablePages")]
        public int AvailablePages => (int)((TotalCount / Take) + ((TotalCount % Take == 0) ? 0 : 1));

        [JsonProperty("currentPage")]
        public int CurrentPage => (Skip + Take) / Take;

        public Paging(int? skip, int? take) {
            Skip = skip ?? 0;
            Take = take ?? 10;
        }
    }
}