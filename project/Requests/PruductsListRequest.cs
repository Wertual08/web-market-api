using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Requests {
    public record ProductsListRequest {
        public int Page { get; init; }
        public List<long> Categories { get; init; } 
        public List<long> Sections { get; init; } 
    }
}