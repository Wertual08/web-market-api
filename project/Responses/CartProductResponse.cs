using Api.Models;

namespace Api.Responses {
    public record CartProductResponse {
        public int Amount { get; init; }
        public ProductResponse Product { get; init; }
    
        public CartProductResponse(CartProduct model) {
            Amount = model.Amount;
            Product = new ProductResponse(model.Product);
        }
    }
}