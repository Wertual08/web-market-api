using Api.Database.Models;

namespace Api.Domain.Responses {
    public record OrderProductResponse {
        public int Amount { get; init; }
        public ProductResponse Product { get; init; }
    
        public OrderProductResponse(OrderProduct model) {
            Amount = model.Amount;
            Product = new ProductResponse(model.Product);
        }
    }
}