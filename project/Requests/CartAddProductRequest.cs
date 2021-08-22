using System.ComponentModel.DataAnnotations;

namespace Api.Requests {
    public class CartAddRequest {
        [Required]
        public long ProductId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}