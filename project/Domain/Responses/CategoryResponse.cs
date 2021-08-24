using System;
using Api.Database.Models;

namespace Api.Domain.Responses {
    public record CategoryResponse {
        public long Id { get; init; }
        public string Name { get; init; }

        public CategoryResponse(Category model) {
            Id = model.Id;
            Name = model.Name;
        }
    }
}