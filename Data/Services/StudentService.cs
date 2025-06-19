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

        //object 들
        public async Task<Result<Student>> GetStudentAsync(int id)
        {
            try
            {
                var student = await _studentRepository.GetStudentAsync(id);
                if (student == null)
                {
                    return Result<Student>.Fail($"No object by {id}");
                }
                return Result<Student>.Ok(student);
            }
            catch (Exception ex)
            {
                return Result<Student>.Fail($"{ex.Message}");
            }
        }



        //List 들


        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentAsync();
        }
        public async Task<Result<List<Student>>> GetAllStudentsAsync1()
        {
            try
            {
                var students = await _studentRepository.GetAllStudentAsync();
                if (students == null || students.Count == 0)
                {
                    return Result<List<Student>>.Fail("등록된 학생이 없습니다.");
                }
                return Result<List<Student>>.Ok(students);
            }
            catch (Exception ex)
            {
                return Result<List<Student>>.Fail(ex.Message);
            }
        }
        public async Task<List<StudentClass>> GetAllClassAsync()
        {
            return await _studentRepository.GetAllClassAsync();
        }
        public async Task<Result<List<StudentClass>>> GetAllClassAsync1()
        {
            try
            {
                var classes = await _studentRepository.GetAllClassAsync();
                if (classes == null || classes.Count == 0)
                {
                    return Result<List<StudentClass>>.Fail("No or Zero class");
                }
                return Result<List<StudentClass>>.Ok(classes);
            }
            catch (Exception ex)
            {
                return Result<List<StudentClass>>.Fail(ex.Message);
            }
        }
        public async Task<List<StudentTime>> GetAllTimesAsync()
        {
            return await _studentRepository.GetAllTimesAsync();
        }
        public async Task<List<StudentTime>> GetTimesByStudentNameAsync(string str)
        {
            return await _studentRepository.GetTimesByStudentNameAsync(str);
        }
        public async Task<List<StudentTime>> GetTimesByStudentClassAsync(string id)
        {
            _ = int.TryParse(id, out int classId);
            return await _studentRepository.GetTimesByStudentClassAsync(classId);
        }
        public async Task<List<StudentTime>> GetOnlyTimesWithClassAsync()
        {
            return await _studentRepository.GetOnlyTimesWithClassAsync();
        }
        public async Task<List<StudentTime>> GetOnlyTimesWithStudentAsync()
        {
            return await _studentRepository.GetOnlyTimesWithStudentAsync();
        }

        //update
        public async Task UpdateAsync()
        {
            await _studentRepository.SaveAsync();
        }


        //CreateModal 에서 씀
        public async Task CreateStudent(string _name, string _description, StudentGrade _StudentGrade, int? _ClassId)
        {
            StudentClass? _Class = null;
            if (_ClassId != null)
            {
                _Class = await _studentRepository.GetClassAsync(_ClassId.Value);
                //if (_Class == null)
                //{
                //    // ❗ 존재하지 않는 ClassId 입력된 경우, 예외 또는 로그 처리
                //    Console.WriteLine($"❌ 잘못된 ClassId: {_ClassId.Value}");
                //    throw new InvalidOperationException($"ClassId {_ClassId.Value}는 존재하지 않습니다.");
                //}
            }

            Student student = new Student()
            {
                Name = _name,
                Description = _description,
                StudentGrade = _StudentGrade,
                ClassId = _ClassId,
                Class = _Class
            };
            try
            {
                await _studentRepository.CreateAsync(student);
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
                await _studentRepository.CreateAsync(klass);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        //ManageModal 에서 씀
        public async Task AddTimeToStudent(int _StudentId, DayOfWeek _DayOfWeek, TimeSpan _StartTime, TimeSpan _EndTime, string _Description)
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
            await _studentRepository.CreateAsync(studentTime);
        }
        public async Task AddTimeToClass(int _ClassId, DayOfWeek _DayOfWeek, TimeSpan _StartTime, TimeSpan _EndTime, string _Description)
        {
            StudentClass? klass = await _studentRepository.GetClassAsync(_ClassId);
            if (klass is null)
                throw new InvalidOperationException("Student not found");

            StudentTime studentTime = new StudentTime()
            {
                ClassId = _ClassId,
                StudentClass = klass,
                DayOfWeek = _DayOfWeek,
                StartTime = _StartTime,
                EndTime = _EndTime,
                Description = _Description
            };
            //student.unableDateTime.Add(studentTime);
            await _studentRepository.CreateAsync(studentTime);
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
                await _studentRepository.DeleteAsync(studentTime);
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
                await _studentRepository.DeleteAsync(student);
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
                await _studentRepository.DeleteAsync(klass);
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
                return Result<bool>.Fail("No object");
            }
            try
            {
                klass.Students.Remove(student);
                await _studentRepository.SaveAsync();
                return Result<bool>.Ok(true, "Success");
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail(ex.Message);
            }
        }
    }

    //public async Task<List<Student>> GetSearchContentAsync(string searchTopic, string searchContent)
    //{
    //    return await _studentRepository.GetSearchContentAsync(searchTopic, searchContent);
    //}












}


