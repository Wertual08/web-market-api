using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Requests {
    public class AdminCategoryCreateRequest {
        [Required]
        [MaxLength(256)]
        public string Name { get; init; }
    }
}