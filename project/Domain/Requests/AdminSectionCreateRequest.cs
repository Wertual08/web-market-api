using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public class AdminSectionCreateRequest {
        public long? SectionId { get; init; }
        
        public long? RecordId { get; init; }

        [Required]
        [MaxLength(256)]
        public string Name { get; init; }
    }
}