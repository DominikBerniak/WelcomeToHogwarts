using Microsoft.EntityFrameworkCore;
using WelcomeToHogwarts.Persistance.Entities;

namespace WelcomeToHogwarts.Persistance.DbSetup
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            string ingredientsPath = Path.GetFullPath(@"Persistance\Data\Ingredients.txt");
            string[] ingredients = File.ReadAllLines(ingredientsPath);
            List<Ingredient> addedIngredients = new List<Ingredient>();
            foreach (var ingredient in ingredients)
            {
                addedIngredients.Add(
                    new Ingredient
                    {
                        Name = ingredient
                    });
            }
            modelBuilder.Entity<Ingredient>().HasData(addedIngredients);

            modelBuilder.Entity<Room>().HasData(new Room
            {
                Id = Guid.Parse("C7D3B3F6-F8C1-4AA2-8363-131E08E09133"),
                Capacity = 4,
                Number = 101
            });

            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = Guid.Parse("C7D3B3F6-F8C1-4AA2-8363-721E08E09133"),
                Name = "Harry Potter",
                HouseType = Enums.HouseType.Gryffindor,
                PetType = Enums.PetType.Owl,
                RoomId = Guid.Parse("C7D3B3F6-F8C1-4AA2-8363-131E08E09133")
            });

            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = Guid.Parse("F0D78492-C790-42E4-8452-9EA645B84800"),
                Name = "Hermione Granger",
                HouseType = Enums.HouseType.Gryffindor,
                PetType = Enums.PetType.Cat
            });
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = Guid.Parse("F0D14492-C790-42E4-8452-9EA645B84800"),
                Name = "Draco Malfoy",
                HouseType = Enums.HouseType.Slytherin,
                PetType = Enums.PetType.None
            });
        }
    }
}
