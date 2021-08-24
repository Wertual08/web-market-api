namespace Api.Domain.Responses {
    public enum ErrorType {
        LoginExists,
        EmailExists,
        PhoneExists,
    }

    public record ErrorResponse {
        public ErrorType Error { get; init; }
    }
}