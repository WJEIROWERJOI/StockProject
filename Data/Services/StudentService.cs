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

        //CreateModal 에서 씀
        public async Task CreateStudent(string _name, string _description, StudentGroup _studentGroup)
        {
            Student student = new Student() { Name = _name, Description = _description, StudentGroup = _studentGroup };
            try
            {
                await _studentRepository.CreateStudent(student);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        //ManageModal 에서 씀
        public async Task AddTimeSlot(int _StudentId, DayOfWeek _DayOfWeek, TimeSpan _StartTime, TimeSpan _EndTime)
        {
            Student? student = await _studentRepository.GetStudentAsync(_StudentId);
            if (student == null)
                throw new InvalidOperationException("Student not found");

            StudentTime studentTime = new StudentTime()
            {
                StudentId = _StudentId,
                Student = student,
                DayOfWeek = _DayOfWeek,
                StartTime = _StartTime,
                EndTime = _EndTime
            };

            await _studentRepository.CreateStudentTime(studentTime);

            student.unableDateTime.Add(studentTime);
        }


        public async Task<bool> DeleteStudentTimeAsync(int id)
        {
           StudentTime? studentTime = await _studentRepository.GetStudentTimeAsync(id);
            if(studentTime is null)
            {
                throw new InvalidOperationException("StudentTime not found");
            }
            try
            {
                await _studentRepository.DeleteStudentTimeAsync(studentTime);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }



    }


}
