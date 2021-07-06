using Api.Models;

namespace Api.Responses {
    public class ProductResponse {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }

        public ProductResponse(Product model) {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
        }
    }
}