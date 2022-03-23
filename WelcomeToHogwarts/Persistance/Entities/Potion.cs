using WelcomeToHogwarts.Persistance.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeToHogwarts.Persistance.Entities
{
    public class Potion : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Student Maker { get; set; }
        public List<Ingredient>? Ingredients { get; set; }

        [Column(TypeName = "varchar(50)"), Required]
        public BrewingStatus BrewingStatus { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
