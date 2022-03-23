using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WelcomeToHogwarts.Persistance.Entities
{
    public class Ingredient : EntityBase
    {
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Potion> Potions { get; set; }
        [JsonIgnore]
        public List<Recipe> Recipes { get; set; }
    }
}
