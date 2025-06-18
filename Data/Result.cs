namespace StockProject.Data
{
    public class Result<T>
    {
        public bool IsSuccess { get; init; }
        public string? Message { get; init; }
        public T? Data { get; init; }
        public static Result<T> Success(T data, string? message = null) =>
            new() { IsSuccess = true, Data = data, Message = message };

        public static Result<T> Failure(string message) =>
            new() { IsSuccess = false, Message = message };
    }

}
