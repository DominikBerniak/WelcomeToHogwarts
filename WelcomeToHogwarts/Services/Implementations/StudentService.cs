using WelcomeToHogwarts.Persistance.DataTransferObjects;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Repositories.Interfaces;
using WelcomeToHogwarts.Services.Interfaces;

namespace WelcomeToHogwarts.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<Room> _roomRepository;

        public StudentService(IGenericRepository<Student> studentRepository, IGenericRepository<Room> roomRepository)
        {
            _studentRepository = studentRepository;
            _roomRepository = roomRepository;
        }
        public async Task<Student> AddStudent(Student student)
        {
            return await _studentRepository.Add(student);
        }

        public async Task<AssignStudentStatusDto> AssignStudentToRoom(AssignStudentDto data)
        {
            var assingStatus = new AssignStudentStatusDto
            {
                RoomNumber = data.RoomNumber,
                StudentName = data.StudentName
            };

            var student = await _studentRepository.GetByProperty(s => s.Name == data.StudentName);
            if (student is null)
            {
                assingStatus.Status = Status.NotFound;
                assingStatus.StatusMessage = "No such student";
                return assingStatus;
            }

            var room = await _roomRepository.GetByProperty(r => r.Number == data.RoomNumber);
            if (room is null)
            {
                assingStatus.Status = Status.NotFound;
                assingStatus.StatusMessage = "No such room";
                return assingStatus;
            }
            else if (room.Residents.Count == room.Capacity)
            {
                assingStatus.Status = Status.BadRequest;
                assingStatus.StatusMessage = "Room full";
                return assingStatus;
            }
            room.Residents.Add(student);
            await _studentRepository.Edit(student);
            assingStatus.Status = Status.Ok;
            assingStatus.StatusMessage = "Successfully assigned student to room";
            return assingStatus;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _studentRepository.GetAll();
        }

        public async Task<Student> GetStudentByName(string studentName)
        {
            return await _studentRepository.GetByProperty(s => s.Name == studentName);
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            return await _studentRepository.Edit(student);
        }
    }
}
