namespace Api.Domain.Services.Result {
    public class ServiceResult<T> {
        public T Data { get; private set; }
        public string Message { get; private set; }
        public ServiceResultStatus Status { get; private set; }

        public ServiceResult(T data) {
            Data = data;
            Message = null;
            Status = ServiceResultStatus.Success;
        }

        public ServiceResult(ServiceResultStatus status, string message = null) {
            Status = status;
            Message = message;
        }

        public static implicit operator ServiceResult<T>(T data) {
			return new ServiceResult<T>(data);
		}

        public static implicit operator ServiceResult<T>(ServiceResultStatus status) {
			return new ServiceResult<T>(status);
		}
    }
}