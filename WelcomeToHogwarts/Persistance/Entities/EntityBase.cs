using System.ComponentModel.DataAnnotations;

namespace WelcomeToHogwarts.Persistance.Entities
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
