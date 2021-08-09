using System;
using Api.Models;

namespace Api.Responses {
    public record CategoryResponse {
        public long Id { get; init; }
        public string Name { get; init; }

        public CategoryResponse(Category model) {
            Id = model.Id;
            Name = model.Name;
        }
    }
}