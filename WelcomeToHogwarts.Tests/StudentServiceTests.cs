using System;
using Xunit;
using NSubstitute;
using WelcomeToHogwarts.Services.Implementations;
using WelcomeToHogwarts.Repositories.Interfaces;
using WelcomeToHogwarts.Persistance.Entities;
using System.Threading.Tasks;
using WelcomeToHogwarts.Persistance.Enums;
using System.Collections.Generic;
using WelcomeToHogwarts.Persistance.DataTransferObjects;
using NSubstitute.ReturnsExtensions;

namespace WelcomeToHogwarts.Tests
{
    public class StudentServiceTests
    {
        private readonly StudentService _sut;
        private readonly IGenericRepository<Student> _studentRepository = Substitute.For<IGenericRepository<Student>>();
        private readonly IGenericRepository<Room> _roomRepository = Substitute.For<IGenericRepository<Room>>();


        public StudentServiceTests()
        {
            _sut = new StudentService(_studentRepository, _roomRepository);
        }

        [Fact]
        public async Task GetStudentByName_ShouldReturnStudent_WhenStudentExists()
        {
            // Arrange
            var studentName = "Harry Potter";
            var houseType = HouseType.Gryffindor;
            var petType = PetType.Owl;
            var roomId = Guid.NewGuid();
            var student = new Student { 
                Name = studentName, 
                HouseType = houseType,
                PetType = petType,
                RoomId = roomId
            };

            _studentRepository.GetByProperty(s => s.Name == studentName).ReturnsForAnyArgs(student);

            // Act
            var result = await _sut.GetStudentByName(studentName);

            // Assert 
            Assert.Equal(studentName, result.Name);
            Assert.Equal(houseType, result.HouseType);
            Assert.Equal(petType, result.PetType);
            Assert.Equal(roomId, result.RoomId);
        }

        [Fact]
        public async Task GetAllStudents_ShouldReturnAllStudent()
        {
            // Arrange
            var student = new Student
            {
                Name = "Harry Potter",
                HouseType = HouseType.Gryffindor,
                PetType = PetType.Owl
            };
            var students = new List<Student>
            {
                student
            };

            _studentRepository.GetAll().Returns(students);

            // Act
            var result = await _sut.GetAllStudents();
            // Assert 

            Assert.True(result.Count > 0);
            Assert.Contains(student, result);
            Assert.Equal(student.Name, result[0].Name);
        }

        [Fact]
        public async Task AssignStudentToRoom_NotExistingStudentFails()
        {
            // Arrange
            var studentData = new AssignStudentDto
            {
                StudentName = "",
                RoomNumber = 1
            };
            var returnStatusData = new AssignStudentStatusDto
            {
                StudentName = studentData.StudentName,
                RoomNumber = studentData.RoomNumber,
                Status = Status.NotFound,
                StatusMessage = "No such student"
            };

            _studentRepository.GetByProperty(s => s.Name == studentData.StudentName).ReturnsNull();
            // Act
            var result = await _sut.AssignStudentToRoom(studentData);
            // Assert 
            Assert.Equal(returnStatusData.StudentName, result.StudentName);
            Assert.Equal(returnStatusData.RoomNumber, result.RoomNumber);
            Assert.Equal(returnStatusData.Status, result.Status);
            Assert.Equal(returnStatusData.StatusMessage, result.StatusMessage);
        }

        [Fact]
        public async Task AssignStudentToRoom_NotExistingRoomFails()
        {
            // Arrange
            var student = new Student
            {
                Name = "Harry Potter",
                HouseType = HouseType.Gryffindor,
                PetType = PetType.Owl
            };

            var studentData = new AssignStudentDto
            {
                StudentName = student.Name,
                RoomNumber = 0
            };
            var returnStatusData = new AssignStudentStatusDto
            {
                StudentName = studentData.StudentName,
                RoomNumber = studentData.RoomNumber,
                Status = Status.NotFound,
                StatusMessage = "No such room"
            };

            _studentRepository.GetByProperty(s => s.Name == studentData.StudentName).ReturnsForAnyArgs(student);
            _roomRepository.GetByProperty(r => r.Number == studentData.RoomNumber).ReturnsNull();
            // Act
            var result = await _sut.AssignStudentToRoom(studentData);
            // Assert 
            Assert.Equal(returnStatusData.StudentName, result.StudentName);
            Assert.Equal(returnStatusData.RoomNumber, result.RoomNumber);
            Assert.Equal(returnStatusData.Status, result.Status);
            Assert.Equal(returnStatusData.StatusMessage, result.StatusMessage);
        }

        [Fact]
        public async Task AssignStudentToRoom_FullRoomFails()
        {
            // Arrange
            var student = new Student
            {
                Name = "Harry Potter",
                HouseType = HouseType.Gryffindor,
                PetType = PetType.Owl
            };
            var residents = new HashSet<Student>();
            residents.Add(student);
            var room = new Room
            {
                Number = 1,
                Capacity = 1,
                Residents = residents
            };

            var studentData = new AssignStudentDto
            {
                StudentName = student.Name,
                RoomNumber = 0
            };
            var returnStatusData = new AssignStudentStatusDto
            {
                StudentName = studentData.StudentName,
                RoomNumber = studentData.RoomNumber,
                Status = Status.BadRequest,
                StatusMessage = "Room full"
            };

            _studentRepository.GetByProperty(s => s.Name == studentData.StudentName).ReturnsForAnyArgs(student);
            _roomRepository.GetByProperty(r => r.Number == studentData.RoomNumber).ReturnsForAnyArgs(room);
            // Act
            var result = await _sut.AssignStudentToRoom(studentData);
            // Assert 
            Assert.Equal(returnStatusData.StudentName, result.StudentName);
            Assert.Equal(returnStatusData.RoomNumber, result.RoomNumber);
            Assert.Equal(returnStatusData.Status, result.Status);
            Assert.Equal(returnStatusData.StatusMessage, result.StatusMessage);
        }

        [Fact]
        public async Task AssignStudentToRoom_ShouldAddNewStudent()
        {
            // Arrange
            var student = new Student
            {
                Name = "Harry Potter",
                HouseType = HouseType.Gryffindor,
                PetType = PetType.Owl
            };
            var residents = new HashSet<Student>();
            var room = new Room
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Capacity = 2,
                Residents = residents
            };

            var studentData = new AssignStudentDto
            {
                StudentName = student.Name,
                RoomNumber = 0
            };
            var returnStatusData = new AssignStudentStatusDto
            {
                StudentName = studentData.StudentName,
                RoomNumber = studentData.RoomNumber,
                Status = Status.Ok,
                StatusMessage = "Successfully assigned student to room"
            };

            _studentRepository.GetByProperty(s => s.Name == studentData.StudentName).ReturnsForAnyArgs(student);
            _roomRepository.GetByProperty(r => r.Number == studentData.RoomNumber).ReturnsForAnyArgs(room);
            // Act
            var result = await _sut.AssignStudentToRoom(studentData);
            // Assert 
            Assert.Equal(returnStatusData.StudentName, result.StudentName);
            Assert.Equal(returnStatusData.RoomNumber, result.RoomNumber);
            Assert.Equal(returnStatusData.Status, result.Status);
            Assert.Equal(returnStatusData.StatusMessage, result.StatusMessage);
            Assert.True(room.Residents.Count == 1);
            Assert.Contains(student, room.Residents);
            Assert.Equal(room.Id, student.RoomId);
        }        
    }
}