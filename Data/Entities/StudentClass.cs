using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StockProject.Data.Entities
{
    public class StudentClass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Student> Students { get; set; } = new();
        public string Description { get; set; } = string.Empty;
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public List<StudentTime> ClassTimes { get; set; } = new();
    }
}
