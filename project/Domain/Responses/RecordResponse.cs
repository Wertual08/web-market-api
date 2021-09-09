using System;
using System.ComponentModel.DataAnnotations;
using Api.Database.Models;

namespace Api.Domain.Responses {
    public record RecordResponse {
        public long Id { get; init; }
        [Required]
        public string Identifier { get; init; }
        [Required]
        public DateTime CreatedAt { get; init; }
        [Required]
        public string ContentType { get; init; }
        [Required]
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