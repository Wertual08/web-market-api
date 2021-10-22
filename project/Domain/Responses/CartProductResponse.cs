using Api.Database.Models;

namespace Api.Domain.Responses {
    public record CartProductResponse {
        public long ProductId { get; init; }
        public int Amount { get; init; }
        public ProductResponse Product { get; init; }
    
        public CartProductResponse(CartProduct model) {
            ProductId = model.ProductId;
            Amount = model.Amount;
        }
    }
}