using Microsoft.AspNetCore.Components.Forms;
using StockProject.Data.Entities;
using StockProject.Data.Repositories;

namespace StockProject.Data.Services
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepository;

        public StudentService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentAsync();
        }
        public async Task<List<StudentClass>> GetAllClassAsync()
        {
            return await _studentRepository.GetAllClassAsync();
        }
        //CreateModal 에서 씀
        public async Task CreateStudent(string _name, string _description, StudentGrade _StudentGrade, int _ClassId)
        {
            StudentClass? _Class = await _studentRepository.GetClassAsync(_ClassId);
            Student student = new Student() { Name = _name, Description = _description, StudentGrade = _StudentGrade, Class = _Class };
            try
            {
                await _studentRepository.CreateStudent(student);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public async Task CreateClass(string _name, string _description)
        {
            if (await _studentRepository.NameExistAsync(_name))
            {
                throw new Exception($"Stock '{_name}' already exists.");
            }

            StudentClass klass = new() { Name = _name, Description = _description };

            try
            {
                await _studentRepository.CreateClass(klass);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        //ManageModal 에서 씀
        public async Task AddTimeSlot(int _StudentId, DayOfWeek _DayOfWeek, TimeSpan _StartTime, TimeSpan _EndTime, string _Description)
        {
            Student? student = await _studentRepository.GetStudentAsync(_StudentId);
            if (student is null)
                throw new InvalidOperationException("Student not found");

            StudentTime studentTime = new StudentTime()
            {
                StudentId = _StudentId,
                Student = student,
                DayOfWeek = _DayOfWeek,
                StartTime = _StartTime,
                EndTime = _EndTime,
                Description = _Description
            };
            //student.unableDateTime.Add(studentTime);
            await _studentRepository.CreateTime(studentTime);
        }
        public async Task<bool> AddStudentToClass(int classId, int studentId)
        {
            var klass = await _studentRepository.GetClassAsync(classId);
            var student = await _studentRepository.GetStudentAsync(studentId);
            if (klass is null || student is null)
            {
                return false;
            }
            if (klass.Students.Contains(student))
            {
                return false;
            }

            try
            {

                klass.Students.Add(student);
                student.Class = klass;
                await _studentRepository.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> DeleteTimeAsync(int id)
        {
            StudentTime? studentTime = await _studentRepository.GetTimeAsync(id);
            if (studentTime is null)
                throw new InvalidOperationException("StudentTime not found");

            try
            {
                await _studentRepository.DeleteTimeAsync(studentTime);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            Student? student = await _studentRepository.GetStudentAsync(id);
            if (student is null)
                throw new InvalidOperationException("Student not found");

            try
            {
                await _studentRepository.DeleteStudentAsync(student);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }
        public async Task<bool> DeleteClassAsync(int id)
        {
            StudentClass? klass = await _studentRepository.GetClassAsync(id);
            if (klass is null)
                throw new InvalidOperationException("Student not found");

            try
            {
                await _studentRepository.DeleteClassAsync(klass);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }
        public async Task<Result<bool>> RemoveStudentFromClass(int classId, int studentId)
        {
            var klass = await _studentRepository.GetClassAsync(classId);
            var student = await _studentRepository.GetStudentAsync(studentId);
            if (klass is null || student is null)
            {
                return Result<bool>.Failure("No object");
            }
            try
            {
                klass.Students.Remove(student);
                await _studentRepository.SaveAsync();
                return Result<bool>.Success(true,"Success");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }

    //public async Task<List<Student>> GetSearchContentAsync(string searchTopic, string searchContent)
    //{
    //    return await _studentRepository.GetSearchContentAsync(searchTopic, searchContent);
    //}












}


