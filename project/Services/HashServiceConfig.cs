namespace Api.Services {
    public record HashServiceConfig {
        public int SaltLength { get; init; } = 16;
        public int IterationCount { get; init; } = 10000;
        public int HashLenght { get; init; } = 48;
    }
}