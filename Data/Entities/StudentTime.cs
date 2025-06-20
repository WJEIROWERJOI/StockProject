using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockProject.Data.Entities
{
    public class StudentTime
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Student")]
        public int? StudentId { get; set; }//student와 연결된거
        public Student? Student { get; set; }//Id 값도 nullable 이여야 합니다잉~~~~
        [ForeignKey("StudentClass")]
        public int? ClassId { get; set; }//class와 연결된거 -> 둘다 null 일 수 있찌만 그렇게 하면 안되용~
        public StudentClass? StudentClass { get; set; }
        public string? Description { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime? SpecificDate { get; set; }

        }
}
