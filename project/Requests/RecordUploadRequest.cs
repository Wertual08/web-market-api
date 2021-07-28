using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Api.Requests {
    public class RecordUploadRequest {
        [Required]
        public IFormFile File { get; init; }
    }
}