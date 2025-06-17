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
        public async Task CreateTime(StudentTime time)
        {
            await _context.StudentTimes.AddAsync(time);
            await _context.SaveChangesAsync();
        }
        public async Task CreateClass(StudentClass clas)
        {
            await _context.StudentClasses.AddAsync(clas);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> NameExistAsync(string str)
        {
            return await _context.StudentClasses.AnyAsync(p => p.Name.ToLower() == str.ToLower());
        }
        //r
        public async Task<List<Student>> GetAllStudentAsync()
        {
            return await _context.Students
                .Include(y=>y.Class)
                .Include(x=>x.unableDateTime)
                .ToListAsync();
        }
        public async Task<List<StudentClass>> GetAllClassAsync()
        {
            return await _context.StudentClasses
                .Include(x=>x.Students)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentAsync(int id)
        {
            return await _context.Students
                .Include(y => y.Class)
                .Include(x=>x.unableDateTime)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<StudentTime?> GetTimeAsync(int id)
        {
            return await _context.StudentTimes
                .Include(s => s.Student)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<StudentClass?> GetClassAsync(int id)
        {
            return await _context.StudentClasses
                .Include(s=>s.Students)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }
        //public async Task<List<Student>> GetSearchContentAsync(string searchTopic, string searchContent)
        //{
        //    return await _context.Students
        //        .Include(y => y.StudentClass)
        //        .Include(x => x.unableDateTime)
        //        .Where(p => EF.Property<string>(p, searchTopic) == searchContent)
        //        .ToListAsync();
        //}
        //u
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        //d
        public async Task DeleteStudentAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTimeAsync(StudentTime studentTime)
        {
            _context.StudentTimes.Remove(studentTime);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteClassAsync(StudentClass studentClass)
        {
            _context.StudentClasses.Remove(studentClass);
            await _context.SaveChangesAsync();
        }


    }
}
