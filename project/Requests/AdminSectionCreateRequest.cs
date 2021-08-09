using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Requests {
    public class AdminSectionCreateRequest {
        public long? SectionId { get; init; }

        [Required]
        [MaxLength(256)]
        public string Name { get; init; }
    }
}