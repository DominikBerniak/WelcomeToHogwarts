using WelcomeToHogwarts.Persistance.DataTransferObjects;
using WelcomeToHogwarts.Persistance.Entities;

namespace WelcomeToHogwarts.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudentByName(string studentName);
        Task<AssignStudentStatusDto> AssignStudentToRoom(AssignStudentDto data);
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(Student student);
    }
}
