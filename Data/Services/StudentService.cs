using Microsoft.AspNetCore.Components.Forms;
using StockProject.Components.ModalPages;
using StockProject.Data.Entities;
using StockProject.Data.Repositories;

namespace StockProject.Data.Services
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepository;
        private readonly LogService _logService;

        public StudentService(StudentRepository studentRepository, LogService logService)
        {
            _studentRepository = studentRepository;
            _logService = logService;
        }




        //R

        //object 들
        public async Task<Result<Student>> GetStudentAsync(int id)
        {
            try
            {
                var student = await _studentRepository.GetStudentAsync(id);
                if (student is null)
                {
                    throw new Exception($"No Object by {id}");
                }
                return Result<Student>.Ok(student);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result<Student>.Fail(ex.Message);
            }
        }

        //List 들
        public async Task<Result<List<Student>>> GetAllStudentsAsync()
        {
            try
            {
                var students = await _studentRepository.GetAllStudentAsync();
                if (students is null)
                {
                    throw new Exception($"object is null");
                }
                return Result<List<Student>>.Ok(students);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result<List<Student>>.Fail(ex.Message);
            }
        }
        public async Task<Result<List<StudentClass>>> GetAllClassAsync()
        {
            try
            {
                var classes = await _studentRepository.GetAllClassAsync();
                if (classes is null)
                {
                    throw new Exception($"object null");
                }
                return Result<List<StudentClass>>.Ok(classes);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result<List<StudentClass>>.Fail(ex.Message);
            }
        }
        public async Task<Result<List<StudentTime>>> GetAllTimesAsync()
        {
            try
            {
                var times = await _studentRepository.GetAllTimesAsync();
                if (times is null)
                {
                    throw new Exception($"object null");
                }
                return Result<List<StudentTime>>.Ok(times);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result<List<StudentTime>>.Fail(ex.Message);
            }
        }
        public async Task<Result<List<Student>>> SearchStudentsAsync(string searchTopic, string searchContent)
        {
            try
            {
                var students = await _studentRepository.SearchStudentsAsync(searchTopic, searchContent);
                //if (!students.Any())이건 널검사가 안됭 (비어있는것만 확인)
                if (students is null)
                {
                    throw new Exception($"object null");
                }
                return Result<List<Student>>.Ok(students);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result<List<Student>>.Fail(ex.Message);
            }
        }
        public async Task<List<StudentTime>> GetTimesByStudentNameAsync(string str)
        {
            return await _studentRepository.GetTimesByStudentNameAsync(str);
        }
        public async Task<List<StudentTime>> GetTimesByStudentClassAsync(string id)
        {
            //int.TryParse(Id, out int classId); 둘다 TryParse 값은 무시한다는 건데 _= 쓰면 명시적으로 무시한다는 뜻
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
        public async Task<Result> UpdateAsync()
        {
            try
            {
                await _studentRepository.SaveAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result.Fail(ex.Message);
            }
        }


        //C
        public async Task<Result> CreateStudent(string _name, string _description, StudentGrade _StudentGrade, int? _ClassId)
        {

            try
            {
                StudentClass? _Class = null;
                if (_ClassId is not null)
                {
                    _Class = await _studentRepository.GetClassAsync(_ClassId.Value);
                    if (_Class is null)
                    {
                        throw new Exception($"No Class by {_ClassId}");
                    }
                }

                Student student = new Student()
                {
                    Name = _name,
                    Description = _description,
                    StudentGrade = _StudentGrade,
                    ClassId = _ClassId,
                    Class = _Class
                };

                await _studentRepository.CreateAsync(student);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> CreateClass(string _name, string _description)
        {
            try
            {
                if (await _studentRepository.NameExistAsync(_name))
                {
                    throw new Exception($"Stock '{_name}' already exists.");
                }

                StudentClass klass = new() { Name = _name, Description = _description };

                await _studentRepository.CreateAsync(klass);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                await _logService.LogErrorAsync(ex.Message);
                return Result.Fail(ex.Message);
            }
        }
        //U?
        public async Task<Result> AddTimeToStudent(int _StudentId, DayOfWeek _DayOfWeek, TimeSpan _StartTime, TimeSpan _EndTime, string _Description)
        {
            try
            {
                Student? student = await _studentRepository.GetStudentAsync(_StudentId);
                if (student is null)
                {
                    throw new Exception($"No student by {_StudentId}");
                }

                StudentTime studentTime = new StudentTime()
                {
                    StudentId = _StudentId,
                    Student = student,
                    DayOfWeek = _DayOfWeek,
                    StartTime = _StartTime,
                    EndTime = _EndTime,
                    Description = _Description
                };

                await _studentRepository.CreateAsync(studentTime);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result.Fail(ex.Message);
            }
        }
        public async Task<Result> AddTimeToClass(int _ClassId, DayOfWeek _DayOfWeek, TimeSpan _StartTime, TimeSpan _EndTime, string _Description)
        {
            try
            {
                StudentClass? klass = await _studentRepository.GetClassAsync(_ClassId);
                if (klass is null)
                {
                    throw new Exception($"No student by {_ClassId}");
                }

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
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result.Fail(ex.Message);
            }
        }
        public async Task<Result> AddStudentToClass(int classId, int studentId)
        {
            try
            {
                var klass = await _studentRepository.GetClassAsync(classId);
                var student = await _studentRepository.GetStudentAsync(studentId);
                if (klass is null || student is null)
                {
                    throw new Exception($"{klass} or {student} null");
                }
                if (klass.Students.Contains(student))
                {
                    throw new Exception($"{student} is already exist in class");
                }

                klass.Students.Add(student);
                student.ClassId = klass.Id;//필요 없는게 있긴할텐데... 명시적으로 확실하게!
                student.Class = klass;

                await _studentRepository.SaveAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result.Fail(ex.Message);
            }
        }

        //D
        public async Task<Result> DeleteTimeAsync(int id)
        {
            try
            {
                StudentTime? studentTime = await _studentRepository.GetTimeAsync(id);
                if (studentTime is null)
                {
                    throw new Exception($"No time by {id}");
                }
                await _studentRepository.DeleteAsync(studentTime);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> DeleteStudentAsync(int id)
        {
            try
            {
                Student? student = await _studentRepository.GetStudentAsync(id);
                if (student is null)
                {
                    throw new InvalidOperationException("Student not found");
                }
                await _studentRepository.DeleteAsync(student);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result.Fail(ex.Message);
            }

        }
        public async Task<Result> DeleteClassAsync(int id)
        {
            try
            {
                StudentClass? klass = await _studentRepository.GetClassAsync(id);
                if (klass is null)
                    throw new InvalidOperationException("Student not found");

                await _studentRepository.DeleteAsync(klass);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result.Fail(ex.Message);
            }
        }
        public async Task<Result> RemoveStudentFromClass(int classId, int studentId)
        {
            try
            {
                var student = await _studentRepository.GetStudentAsync(studentId);
                if (student is null)
                {
                    throw new Exception($"No student by {studentId}");
                }
                if (student.ClassId == classId)
                {
                    student.ClassId = null;
                    student.Class = null;
                }
                await _studentRepository.SaveAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(ex.Message);
                return Result.Fail(ex.Message);
            }
        }

    }













}


