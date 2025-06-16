using System.ComponentModel.DataAnnotations;

namespace StockProject.Data.Entities
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public StudentGroup StudentGroup { get; set; } = StudentGroup.Others;
        public List<StudentTime> unableDateTime { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
