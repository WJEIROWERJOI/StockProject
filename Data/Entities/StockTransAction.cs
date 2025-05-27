
using System.ComponentModel.DataAnnotations;

namespace StockProject.Data.Entities;
public class StockTransaction
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string StockId { get; set; }//물건 ID
    // public required string UserId { get; set; }//담당자? ID
    public required TransactionType Type { get; set; }// In= 0 , Out = 1, Change = 2
    public required int Quantity { get; set; }//양
    public required int PreviousQuantity { get; set; }
    public required int AfterQuantity { get; set; }
    public string Remark { get; set; } = string.Empty; // 메모
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    
}

