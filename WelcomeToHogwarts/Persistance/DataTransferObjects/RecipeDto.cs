using WelcomeToHogwarts.Persistance.Entities;

namespace WelcomeToHogwarts.Persistance.DataTransferObjects
{
    public class RecipeDto
    {
    }
    public class GetRecipesStatusDto
    {
        public List<Recipe> Recipes { get; set; }
        public Status Status { get; set; }
        public string StatusMessage { get; set; }
    }
}
