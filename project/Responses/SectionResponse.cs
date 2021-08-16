using Api.Models;

namespace Api.Responses {
    public record SectionResponse {
        public long Id { get; init; }
        public string Name { get; init; }

        public SectionResponse(Section model) {
            Id = model.Id;
            Name = model.Name;
        }
    }
}