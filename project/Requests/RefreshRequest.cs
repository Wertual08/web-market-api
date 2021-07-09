namespace Api.Requests {
    public record RefreshRequest {
        public string Token { get; init; }
    }
}