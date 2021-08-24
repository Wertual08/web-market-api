using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Requests {
    public class AdminSectionUpdateRequest {
        public long? SectionId { get; init; }
        
        public long? RecordId;

        [Required]
        [MaxLength(256)]
        public string Name { get; init; }
    }
}