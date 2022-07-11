using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpectrumTV.Models.Responses
{
    public class SearchResultsResponse<T>
    {
        [JsonProperty("results")]
        public IReadOnlyList<T> Results { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }
}
