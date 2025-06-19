namespace StockProject.Data
{
    public class Result<T>
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public T? Data { get; init; }
        public static Result<T> Ok(T data, string? message = null) =>
            new() { Success = true, Data = data, Message = message };

        public static Result<T> Fail(string message) =>
            new() { Success = false, Message = message };
    }

}
