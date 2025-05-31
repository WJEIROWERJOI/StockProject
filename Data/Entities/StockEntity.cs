using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockProject.Data.Entities;

public class StockEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("CategoryId")]
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public StockCategory? Category { get; set; }
    public required string ProductName { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    public List<StockTransaction> Transactions { get; set; } = new();
}

