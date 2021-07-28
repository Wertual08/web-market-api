using System;
using Api.Models;

namespace Api.Responses {
    public record RecordResponse {
        public long Id { get; init; }
        public string Identifier { get; init; }
        public DateTime CreatedAt { get; init; }
        public string ContentType { get; init; }
        public string Name { get; init; }

        public RecordResponse(Record model) {
            Id = model.Id;
            Identifier = model.Identifier.ToString("N");
            CreatedAt = model.CreatedAt;
            ContentType = model.ContentType;
            Name = model.Name;
        }
    }
}