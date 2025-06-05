using System.ComponentModel.DataAnnotations;

namespace StockProject.Data.Entities
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public List<Board> Boards { get; set; } = new();
        public string Img { get; set; } = string.Empty;
        public bool Primary { get; set; }

    }
}
