using System.Collections.Generic;
using Api.Models;

namespace Api.Responses {
    public record ProductResponse {
        public long Id { get; init; }
        public decimal Price { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public List<string> Records { get; init; }

        public ProductResponse(Product model) {
            Id = model.Id;
            Price = model.Price;
            Name = model.Name;
            Description = model.Description;
            Records = new ();

            if (model.Records is not null) {
                foreach (var record in model.Records) {
                    Records.Add(record.Identifier.ToString("N"));
                }
            }
        }
    }
}