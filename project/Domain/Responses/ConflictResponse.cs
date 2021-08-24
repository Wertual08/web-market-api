using System;
using Api.Database.Models;

namespace Api.Domain.Responses {
    public record ConflictResponse {
        public string Field { get; init; }
        public ConflictResponse(string field) {
            Field = field;
        }
    }
}