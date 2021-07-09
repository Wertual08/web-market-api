namespace Api.Responses {
    public record AuthorizationResponse {
        public string RefreshToken { get; init; }
        public string AccessToken { get; init; }
    }
}