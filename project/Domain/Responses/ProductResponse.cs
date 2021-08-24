using System.Collections.Generic;
using Api.Database.Models;

namespace Api.Domain.Responses {
    public record ProductResponse {
        public long Id { get; init; }
        public decimal Price { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public List<string> Records { get; init; }
        public List<CategoryResponse> Categories { get; init; }
        public List<SectionResponse> Sections { get; init; }

        public ProductResponse(Product model) {
            Id = model.Id;
            Price = model.Price;
            Name = model.Name;
            Description = model.Description;
            Records = new ();
            Categories = new ();
            Sections = new ();

            if (model.Records is not null) {
                foreach (var record in model.Records) {
                    Records.Add(record.Identifier.ToString("N"));
                }
            }
            if (model.Categories is not null) {
                foreach (var category in model.Categories) {
                    Categories.Add(new CategoryResponse(category));
                }
            }
            if (model.Sections is not null) {
                foreach (var section in model.Sections) {
                    Sections.Add(new SectionResponse(section));
                }
            }
        }
    }
}