namespace WelcomeToHogwarts.Persistance.Entities
{
    public class Room : EntityBase
    {
        public int Capacity { get; set; }
        public int Number { get; set; }
        public HashSet<Student> Residents { get; set; } = new HashSet<Student>();
    }
}
