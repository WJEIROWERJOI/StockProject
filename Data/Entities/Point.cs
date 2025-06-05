
namespace StockProject.Data.Entities;
public class Point
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public UserEntity User { get; set; } = null!;
}