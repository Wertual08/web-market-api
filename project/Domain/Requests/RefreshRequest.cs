namespace Api.Domain.Requests {
    public record RefreshRequest {
        public string Token { get; init; }
    }
}