namespace StockProject.Data.Dtos
{
    public record UserLoginDto
    {
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; }
    }
}
