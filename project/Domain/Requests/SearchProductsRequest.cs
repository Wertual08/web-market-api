using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public record SearchProductsRequest {
        public string Query { get; init; }
        public int Page { get; init; }
        public List<long> Categories { get; init; } 
        public List<long> Sections { get; init; } 
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
    }
}