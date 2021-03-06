using System.Collections.Generic;
using System.Linq;
using Api.Database.Models;
using Api.FullTextSearch.Models;

namespace Api.Domain.Responses {
    public record LiteProductResponse {
        public long Id { get; init; }
        public decimal? OldPrice { get; init; }
        public decimal Price { get; init; }
        public string Code { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Image { get; init; }
        public List<long> Categories { get; init; }
        public List<long> Sections { get; init; }

        public LiteProductResponse(FTSProduct model) {
            Id = model.Id;
            OldPrice = model.OldPrice;
            Price = model.Price;
            Code = model.Code;
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

        public LiteProductResponse(Product model) {
            Id = model.Id;
            OldPrice = model.OldPrice;
            Price = model.Price;
            Code = model.Code;
            Name = model.Name;
            Description = model.Description;
            Image = model.Records.FirstOrDefault()?.Identifier.ToString("N");
            Categories = new ();
            Sections = new ();

            if (model.Categories is not null) {
                foreach (var category in model.Categories) {
                    Categories.Add(category.Id);
                }
            }
            if (model.Sections is not null) {
                foreach (var section in model.Sections) {
                    Sections.Add(section.Id);
                }
            }
        }
    }
}