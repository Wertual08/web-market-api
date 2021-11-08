using System;
using Api.Database.Models;

namespace Api.Domain.Responses {
    public class ReviewResponse {
        public long Id { get; init; }
        public int? Grade { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string Description { get; init; }
        public DateTime CreatedAt { get; init; }

        public ReviewResponse(Review model) {
            Id = model.Id;
            Grade = model.Grade;
            Name = model.Name;
            Address = model.Address;
            Description = model.Description;
            CreatedAt = model.CreatedAt;
        }
    }
}