using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Requests {
    public class AdminCategoryUpdateRequest {
        [Required]
        [MaxLength(256)]
        public string Name { get; init; }
    }
}