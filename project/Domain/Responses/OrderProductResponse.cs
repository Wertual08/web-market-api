using Api.Database.Models;

namespace Api.Domain.Responses {
    public record OrderProductResponse {
        public int Amount { get; init; }
        public decimal Price { get; init; }
        public LiteProductResponse Product { get; init; }
    
        public OrderProductResponse(OrderProduct model) {
            Amount = model.Amount;
            Price = model.Price;
            Product = new LiteProductResponse(model.Product);
        }
    }
}