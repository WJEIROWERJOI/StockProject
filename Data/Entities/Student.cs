using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockProject.Data.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public StudentGrade StudentGrade { get; set; } = StudentGrade.Others;
        public List<StudentTime> unableDateTime { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("StudentClass")]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public StudentClass? Class { get; set; }
    }
}
