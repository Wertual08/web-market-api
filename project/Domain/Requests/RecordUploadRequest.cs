using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Api.Domain.Requests {
    public class RecordUploadRequest {
        [Required]
        public List<IFormFile> Files { get; init; }
    }
}