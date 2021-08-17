using System;
using Api.Models;

namespace Api.Responses {
    public record ConflictResponse {
        public string Field { get; init; }
        public ConflictResponse(string field) {
            Field = field;
        }
    }
}