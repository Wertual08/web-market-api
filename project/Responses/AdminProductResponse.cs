using System;
using System.Collections.Generic;
using Api.Models;

namespace Api.Responses {
    public record AdminProductResponse {
        public long Id { get; init; }
        public decimal Price { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public List<string> Records { get; init; }
        public List<AdminCategoryResponse> Categories { get; init; }
        public List<AdminSectionResponse> Sections { get; init; }

        public AdminProductResponse(Product model) {
            Id = model.Id;
            Price = model.Price;
            Name = model.Name;
            Description = model.Description;
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
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
                    Categories.Add(new AdminCategoryResponse(category));
                }
            }
            if (model.Sections is not null) {
                foreach (var section in model.Sections) {
                    Sections.Add(new AdminSectionResponse(section));
                }
            }
        }
    }
}