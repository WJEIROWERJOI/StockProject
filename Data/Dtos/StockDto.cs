using StockProject.Data.Entities;

namespace StockProject.Data.Dtos
{
    public record StockDto
    {
        public StockCategory? Category { get; init; }
        public required string ProductName { get; init; }
        public string Description { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public DateTime LastUpdatedAt { get; init; }

    }
}
