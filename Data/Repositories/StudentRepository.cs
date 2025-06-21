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

        // 다 가져오기
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
        // 조건으로 가져오기
        private IQueryable<Student> StudentsWithIncludes() =>
            _context.Students
                .Include(x => x.Class)
                .Include(x => x.unableDateTime);

        public async Task<List<Student>> SearchStudentsAsync(string topic, string content)
        {
            var query = StudentsWithIncludes();

            query = topic switch
            {
                "Name" => query.Where(x => x.Name.ToLower() == content.ToLower()),
                "StudentGrade" => query.Where(x => x.StudentGrade == Enum.Parse<StudentGrade>(content)),
                "ClassId" => query.Where(x => x.ClassId == int.Parse(content)),
                _ => query.Where(x => false) // invalid topic
            };

            return await query.ToListAsync();
        }
        private IQueryable<StudentTime> TimesWithIncludes() =>
            _context.StudentTimes
                .Include(x => x.StudentClass)
                .Include(x => x.Student);
        public async Task<List<StudentTime>> SearchTimesAsync(string topic, string content)
        {
            var query = TimesWithIncludes();

            query = topic switch
            {
                "Name" => query.Where(x => x.Student == null || x.Student.Name.ToLower() == content.ToLower()),
                "StudentGrade" => query.Where(x => x.Student == null || x.Student.StudentGrade == Enum.Parse<StudentGrade>(content)),
                "ClassId" => query.Where(x => x.Student == null || x.Student.ClassId == int.Parse(content)),
                _ => query.Where(x => false) // invalid topic
            };

            return await query.ToListAsync();
        }



        //하나 가져오기
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
                .Include(x => x.StudentClass)
                .Include(x => x.Student)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<StudentClass?> GetClassAsync(int id)
        {
            return await _context.StudentClasses
                .Include(x => x.ClassTimes)
                .Include(x => x.Students)
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
