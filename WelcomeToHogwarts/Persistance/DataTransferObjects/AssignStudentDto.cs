namespace WelcomeToHogwarts.Persistance.DataTransferObjects
{
    public class AssignStudentDto
    {
        public string StudentName { get; set; }
        public int RoomNumber { get; set; }
    }
    public class AssignStudentStatusDto : AssignStudentDto
    {
        public Status Status { get; set; }
        public string StatusMessage { get; set; }
    }
}
