using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using StockProject.Data.Entities;

namespace StockProject.Data.Dtos
{
    public class StockDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? UserId { get; set; }
        [Required(ErrorMessage = "No Category selected.")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product name is required.")]
        public string ProductName { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Cannot be less than 0.")]
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public bool IsEditing { get; set; }
    }
}
