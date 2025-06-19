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
        public async Task CreateAsync<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        //r
        public async Task<bool> NameExistAsync(string str)
        {
            return await _context.StudentClasses
                .AnyAsync(p => p.Name.ToLower() == str.ToLower());
        }
        public async Task<List<Student>> GetAllStudentAsync()
        {
            return await _context.Students
                .Include(x => x.Class)
                .Include(x => x.unableDateTime)
                .ToListAsync();
        }
        public async Task<List<StudentClass>> GetAllClassAsync()
        {
            return await _context.StudentClasses
                .Include(x => x.ClassTimes)
                .Include(x => x.Students)
                .ToListAsync();
        }
        public async Task<List<StudentTime>> GetAllTimesAsync()
        {
            return await _context.StudentTimes
                .Include(x => x.StudentClass)
                .Include(x => x.Student)
                .ToListAsync();
        }
        public async Task<List<StudentTime>> GetOnlyTimesWithClassAsync()
        {

            return await _context.StudentTimes
                .Include(x => x.StudentClass)
                .Include(x => x.Student)
                .Where(x => x.StudentId == null && x.ClassId != null)
                .ToListAsync();
        }
        public async Task<List<StudentTime>> GetOnlyTimesWithStudentAsync()
        {
            return await _context.StudentTimes
                .Include(x => x.StudentClass)
                .Include(x => x.Student)
                .Where(x => x.StudentId != null && x.ClassId == null)
                .ToListAsync();
        }

        public async Task<List<StudentTime>> GetTimesByStudentNameAsync(string str)
        {
            return await _context.StudentTimes
                .Where(x => x.Student != null && x.Student.Name.ToLower() == str.ToLower())
                .ToListAsync();
        }
        public async Task<List<StudentTime>> GetTimesByStudentClassAsync(int id)
        {
            return await _context.StudentTimes
                .Where(x => x.ClassId != null && x.ClassId == id)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentAsync(int id)
        {
            return await _context.Students
                .Include(x => x.Class)
                .Include(x => x.unableDateTime)
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
                .Include(s => s.Students)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        //u
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        //d
        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
