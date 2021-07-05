using System;

namespace api.Controllers.Admin.Products {
    public class ProductResponse {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}