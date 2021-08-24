using System;
using System.Collections.Generic;
using Api.Database.Models;

namespace Api.Domain.Responses {
    public record AdminSectionResponse {
        public long Id { get; init; }
        public long? ParentId { get; init; }
        public string Name { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }

        public string Record { get; init; }
        public List<AdminSectionResponse> Sections { get; init; }

        public AdminSectionResponse(Section model) {
            Id = model.Id;
            ParentId = model.SectionId;
            Name = model.Name;
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
            Record = model.Record?.Identifier.ToString("N");
            Sections = new List<AdminSectionResponse>();
            if (model.Sections is not null) {
                foreach (var section in model.Sections) {
                    Sections.Add(new AdminSectionResponse(section));
                }
            }
        }
    }
}