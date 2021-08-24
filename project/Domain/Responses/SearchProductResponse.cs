using System.Collections.Generic;
using Api.FullTextSearch.Models;

namespace Api.Domain.Responses {
    public record SearchProductResponse {
        public long Id { get; init; }
        public decimal Price { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Image { get; init; }
        public List<long> Categories { get; init; }
        public List<long> Sections { get; init; }

        public SearchProductResponse(FTSProduct model) {
            Id = model.Id;
            Price = model.Price;
            Name = model.Name;
            Description = model.Description;
            Image = model.Image;
            Categories = new ();
            Sections = new ();

            if (model.Categories is not null) {
                foreach (var category in model.Categories) {
                    Categories.Add(category);
                }
            }
            if (model.Sections is not null) {
                foreach (var section in model.Sections) {
                    Sections.Add(section);
                }
            }
        }
    }
}