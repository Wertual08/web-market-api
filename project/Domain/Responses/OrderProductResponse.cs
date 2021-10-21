using Api.Database.Models;

namespace Api.Domain.Responses {
    public record OrderProductResponse {
        public int Amount { get; init; }
        public decimal Price { get; init; }
        public ProductResponse Product { get; init; }
    
        public OrderProductResponse(OrderProduct model) {
            Amount = model.Amount;
            Price = model.Price;
            Product = new ProductResponse(model.Product);
        }
    }
}