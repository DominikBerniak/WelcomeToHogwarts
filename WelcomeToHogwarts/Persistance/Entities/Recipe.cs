using System.ComponentModel.DataAnnotations;

namespace WelcomeToHogwarts.Persistance.Entities
{
    public class Recipe : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Student Maker { get; set; }
        public Guid MakerId { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
