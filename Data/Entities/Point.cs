
namespace StockProject.Data.Entities;
public class Point
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = "Temp";
    public int Money { get; set; } = 100;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    // public UserEntity User { get; set; } = null!;
}