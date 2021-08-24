using System;
using Api.Database.Models;

namespace Api.Domain.Responses {
    public record AdminCategoryResponse {
        public long Id { get; init; }
        public string Name { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }

        public AdminCategoryResponse(Category model) {
            Id = model.Id;
            Name = model.Name;
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
        }
    }
}