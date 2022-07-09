using System;
using Newtonsoft.Json;

namespace SpectrumTV.Models.Responses
{
    public class SearchResultsResponse<T>
    {
        [JsonProperty("results")]
        public string Results { get; set; }

        [JsonProperty("page")]
        public bool Page { get; set; }

        [JsonProperty("total_pages")]
        public bool TotalPages { get; set; }

        [JsonProperty("total_results")]
        public bool TotalResults { get; set; }
    }
}
