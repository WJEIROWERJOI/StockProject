using Microsoft.EntityFrameworkCore;
using StockProject.Data.Entities;

namespace StockProject.Data.Repositories
{
    public class StudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        //c
        public async Task CreateStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }
        public async Task CreateStudentTime(StudentTime time)
        {
            await _context.StudentTimes.AddAsync(time);
            await _context.SaveChangesAsync();
        }
        //r
        public async Task<List<Student>> GetAllStudentAsync()
        {
            return await _context.Students
                .Include(x=>x.unableDateTime)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentAsync(int id)
        {
            return await _context.Students
                //.AsTracking()
                .Include(x=>x.unableDateTime)
                .FirstOrDefaultAsync(s => s.id == id);
        }
        public async Task<StudentTime?> GetStudentTimeAsync(int id)
        {
            return await _context.StudentTimes
                .Include(s => s.Student)
                .FirstOrDefaultAsync(x => x.id == id);
        }
        //u
        public async Task UpdateStudent()
        {
            await _context.SaveChangesAsync();
        }
        //d
        public async Task DeleteStudentAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteStudentTimeAsync(StudentTime studentTime)
        {
            _context.StudentTimes.Remove(studentTime);
            await _context.SaveChangesAsync();
        }



    }
}
