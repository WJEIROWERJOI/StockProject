using Microsoft.AspNetCore.Http.HttpResults;
using StockProject.Data.Entities;

namespace StockProject.Data.Dtos
{
    public class StockDto
    {
        public required string Id { get; set; } = Guid.NewGuid().ToString();

        public string? UserId { get; set; }
        public required string Category { get; set; }
        public required string ProductName { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public bool IsEditing { get; set; }
    }
}
