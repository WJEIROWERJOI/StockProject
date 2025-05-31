using System.ComponentModel.DataAnnotations;

namespace StockProject.Data.Entities;

public class StockCategory
{
    [Key]
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<StockEntity> Stocks { get; set; } = new();
}

