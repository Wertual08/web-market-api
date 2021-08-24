using System.Collections.Generic;
using Api.Database.Models;

namespace Api.Domain.Responses {
    public record SectionResponse {
        public long Id { get; init; }
        public long? ParentId { get; init; }
        public string Name { get; init; }
        public string Record { get; init; }
        public List<SectionResponse> Sections { get; init; }

        public SectionResponse(Section model) {
            Id = model.Id;
            ParentId = model.SectionId;
            Name = model.Name;

            Record = model.Record?.Identifier.ToString("N");
            Sections = new List<SectionResponse>();
            if (model.Sections is not null) {
                foreach (var section in model.Sections) {
                    Sections.Add(new SectionResponse(section));
                }
            }
        }
    }
}