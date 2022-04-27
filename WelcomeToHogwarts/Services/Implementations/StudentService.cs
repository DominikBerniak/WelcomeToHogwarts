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
            var assignStatus = new AssignStudentStatusDto
            {
                RoomNumber = data.RoomNumber,
                StudentName = data.StudentName
            };

            var student = await _studentRepository.GetByProperty(s => s.Name == data.StudentName);
            if (student is null)
            {
                assignStatus.Status = Status.NotFound;
                assignStatus.StatusMessage = "No such student";
                return assignStatus;
            }

            var room = await _roomRepository.GetByProperty(r => r.Number == data.RoomNumber);
            if (room is null)
            {
                assignStatus.Status = Status.NotFound;
                assignStatus.StatusMessage = "No such room";
                return assignStatus;
            }
            else if (room.Residents.Count == room.Capacity)
            {
                assignStatus.Status = Status.BadRequest;
                assignStatus.StatusMessage = "Room full";
                return assignStatus;
            }
            room.Residents.Add(student);
            student.RoomId = room.Id;
            await _studentRepository.Edit(student);
            assignStatus.Status = Status.Ok;
            assignStatus.StatusMessage = "Successfully assigned student to room";
            return assignStatus;
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
