using System.Collections.Generic;
using Api.Models;

namespace Api.Responses {
    public record SectionResponse {
        public long Id { get; init; }
        public long? ParentId { get; init; }
        public string Name { get; init; }

        public List<SectionResponse> Sections { get; init; }

        public SectionResponse(Section model) {
            Id = model.Id;
            ParentId = model.SectionId;
            Name = model.Name;

            Sections = new List<SectionResponse>();
            if (model.Sections is not null) {
                foreach (var section in model.Sections) {
                    Sections.Add(new SectionResponse(section));
                }
            }
        }
    }
}