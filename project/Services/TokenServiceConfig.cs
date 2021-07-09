namespace Api.Services {
    public record TokenServiceConfig {
        public string Secret { get; init; } = "";
        //public int RefreshLifetime { get; init; } = 31556952;
        public int AccessLifetime { get; init; } = 900;
    }
}