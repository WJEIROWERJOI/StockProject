using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockProject.Data.Entities
{
    public class StudentTime
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public required Student Student { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime? SpecificDate { get; set; }

        }
}
