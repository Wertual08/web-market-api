namespace Api.Domain.Responses {
    public record AuthorizationResponse {
        public string RefreshToken { get; init; }
        public string AccessToken { get; init; }
        public long ExpiresAt { get; init; }
    }
}