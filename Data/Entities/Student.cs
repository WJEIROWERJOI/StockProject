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
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public List<StudentTime> unableDateTime { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("StudentClass")]
        public int? ClassId { get; set; }
        public StudentClass? Class { get; set; }
        //개인정보, 다른학원시간, 실제 수업시간, 출결 사항, 과제 수행률 등 -> TextArea로?
        //public string Memo {get;set;} = string.Empty;

    }
}
