using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Repositories.Interfaces;
using NSubstitute;
using WelcomeToHogwarts.Services.Implementations;
using System;
using Xunit;
using WelcomeToHogwarts.Persistance.DataTransferObjects;
using NSubstitute.ReturnsExtensions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using WelcomeToHogwarts.Persistance.Enums;
using System.Diagnostics.Metrics;
using WelcomeToHogwarts.Repositories.Implementations;

namespace WelcomeToHogwarts.Tests
{
    public class PotionServiceTests
    {
        private readonly PotionService _sut;
        private readonly IPotionRepository _potionRepository = Substitute.For<IPotionRepository>();
        private readonly IRecipeRepository _recipeRepository = Substitute.For<IRecipeRepository>();
        private readonly IGenericRepository<Student> _studentRepository = Substitute.For<IGenericRepository<Student>>();
        private readonly IGenericRepository<Ingredient> _ingredientRepository = Substitute.For<IGenericRepository<Ingredient>>();

        public PotionServiceTests()
        {
            _sut = new PotionService(_potionRepository, _recipeRepository, _studentRepository, _ingredientRepository);
        }

        [Fact]
        public async Task AddIngredientToPotion_NoPotionShouldFail()
        {
            // Arrange
            var ingredientData = new IngredientDto();

            var status = new CreatePotionStatusDto
            {
                Status = Status.NotFound,
                StatusMessage = "No such potion"
            };
            var potionId = Guid.NewGuid();

            _potionRepository.GetPotionByProperty(p => p.Id == potionId).ReturnsNull();

            // Act
            var result = await _sut.AddIngredientToPotion(potionId, ingredientData);

            // Assert 
            Assert.Equal(status.Status, result.Status);
            Assert.Equal(status.StatusMessage, result.StatusMessage);
        }

        [Fact]
        public async Task AddIngredientToPotion_DonePotionShouldFail()
        {
            // Arrange
            var ingredientData = new IngredientDto();
            var potionId = Guid.NewGuid();
            var potion = new Potion
            {
                Id = potionId,
                Name = "Potion",
                Maker = new Student(),
                Ingredients = new List<Ingredient>
                {
                    new Ingredient {Name = "1"},
                    new Ingredient {Name = "2"},
                    new Ingredient {Name = "3"},
                    new Ingredient {Name = "4"},
                    new Ingredient {Name = "5"},
                }
            };

            var status = new CreatePotionStatusDto
            {
                Status = Status.BadRequest,
                StatusMessage = "Potion is already done"
            };


            _potionRepository.GetPotionByProperty(p => p.Id == potionId).ReturnsForAnyArgs(potion);

            // Act
            var result = await _sut.AddIngredientToPotion(potionId, ingredientData);

            // Assert 
            Assert.Equal(status.Status, result.Status);
            Assert.Equal(status.StatusMessage, result.StatusMessage);
        }

        [Fact]
        public async Task AddIngredientToPotion_NewIngredientIsAdded_PotionIsUpdated()
        {
            // Arrange
            var ingredientData = new IngredientDto { Name = "3"};
            var potionId = Guid.NewGuid();
            var potion = new Potion
            {
                Id = potionId,
                Name = "Potion",
                Maker = new Student(),
                Ingredients = new List<Ingredient>
                {
                    new Ingredient {Name = "1"},
                    new Ingredient {Name = "2"}
                }
            };

            var status = new CreatePotionStatusDto
            {
                Status = Status.Updated,
                StatusMessage = "Successfully added a ingredient to this potion"
            };


            _potionRepository.GetPotionByProperty(p => p.Id == potionId).ReturnsForAnyArgs(potion);
            _ingredientRepository.GetByProperty(i => i.Name == ingredientData.Name).ReturnsNull();

            // Act
            var result = await _sut.AddIngredientToPotion(potionId, ingredientData);

            // Assert 
            Assert.Equal(status.Status, result.Status);
            Assert.Equal(status.StatusMessage, result.StatusMessage);
            Assert.True(result.Potion.Ingredients.Count == 3);
            Assert.Equal(ingredientData.Name, result.Potion.Ingredients[2].Name);
        }

        [Fact]
        public async Task AddIngredientToPotion_PotionIsUpdated()
        {
            // Arrange
            var ingredientData = new IngredientDto { Name = "3" };
            var potionId = Guid.NewGuid();
            var potion = new Potion
            {
                Id = potionId,
                Name = "Potion",
                Maker = new Student(),
                Ingredients = new List<Ingredient>
                {
                    new Ingredient {Name = "1"},
                    new Ingredient {Name = "2"}
                }
            };

            var ingredient = new Ingredient
            {
                Name = ingredientData.Name
            };

            var status = new CreatePotionStatusDto
            {
                Status = Status.Updated,
                StatusMessage = "Successfully added a ingredient to this potion"
            };


            _potionRepository.GetPotionByProperty(p => p.Id == potionId).ReturnsForAnyArgs(potion);
            _ingredientRepository.GetByProperty(i => i.Name == ingredientData.Name).ReturnsForAnyArgs(ingredient);

            // Act
            var result = await _sut.AddIngredientToPotion(potionId, ingredientData);

            // Assert 
            Assert.Equal(status.Status, result.Status);
            Assert.Equal(status.StatusMessage, result.StatusMessage);
            Assert.True(result.Potion.Ingredients.Count == 3);
            Assert.Equal(ingredientData.Name, result.Potion.Ingredients[2].Name);
            Assert.Contains(ingredient, result.Potion.Ingredients);
        }


        [Fact]
        public async Task AddIngredientToPotion_DonePotionWithSameIngredientsAsRecipeShouldBeMarkAsReplica()
        {
            // Arrange
            var ingredientData = new IngredientDto { Name = "5" };
            var potionId = Guid.NewGuid();
            var ingredient1 = new Ingredient { Name = "1" };
            var ingredient2 = new Ingredient { Name = "2" };
            var ingredient3 = new Ingredient { Name = "3" };
            var ingredient4 = new Ingredient { Name = "4" };
            var potion = new Potion
            {
                Id = potionId,
                Name = "Potion",
                Maker = new Student(),
                Ingredients = new List<Ingredient>
                {
                    ingredient1, ingredient2, ingredient3, ingredient4
                }
            };

            var newIngredient = new Ingredient
            {
                Name = ingredientData.Name
            };

            var recipe = new Recipe
            {
                Name = "Recipe",
                Maker = new Student(),
                Ingredients = new List<Ingredient>
                {
                    ingredient1, ingredient2, ingredient3, ingredient4, newIngredient
                }
            };
            var recipes = new List<Recipe> { recipe };

            var status = new CreatePotionStatusDto
            {
                Status = Status.Ok,
                StatusMessage = "Your potions is done"
            };


            _potionRepository.GetPotionByProperty(p => p.Id == potionId).ReturnsForAnyArgs(potion);
            _ingredientRepository.GetByProperty(i => i.Name == ingredientData.Name).ReturnsForAnyArgs(newIngredient);
            _recipeRepository.GetAllRecipes().Returns(recipes);

            // Act
            var result = await _sut.AddIngredientToPotion(potionId, ingredientData);

            // Assert 
            Assert.Equal(status.Status, result.Status);
            Assert.Equal(status.StatusMessage, result.StatusMessage);
            Assert.True(result.Potion.Ingredients.Count == 5);
            Assert.Equal(ingredientData.Name, result.Potion.Ingredients[4].Name);
            Assert.Contains(newIngredient, result.Potion.Ingredients);
            Assert.Equal(BrewingStatus.Replica, result.Potion.BrewingStatus);
            Assert.Equal(recipe, result.Potion.Recipe);
        }

        [Fact]
        public async Task AddIngredientToPotion_DonePotionWithNewIngredientsShouldBeMarkAsDiscovery()
        {
            // Arrange
            var ingredientData = new IngredientDto { Name = "5" };
            var potionId = Guid.NewGuid();
            var ingredient1 = new Ingredient { Name = "1" };
            var ingredient2 = new Ingredient { Name = "2" };
            var ingredient3 = new Ingredient { Name = "3" };
            var ingredient4 = new Ingredient { Name = "4" };
            var potion = new Potion
            {
                Id = potionId,
                Name = "Potion",
                Maker = new Student
                {
                    Name = "Harry Potter"
                },
                Ingredients = new List<Ingredient>
                {
                    ingredient1, ingredient2, ingredient3, ingredient4
                }
            };

            var newIngredient = new Ingredient
            {
                Name = ingredientData.Name
            };

            var recipe = new Recipe
            {
                Name = "Recipe",
                Maker = new Student(),
                Ingredients = new List<Ingredient>
                {
                    ingredient1, ingredient2, ingredient3, ingredient4,
                    new Ingredient { Name = "Different" }
                }
            };
            var recipes = new List<Recipe> { recipe };

            var status = new CreatePotionStatusDto
            {
                Status = Status.Ok,
                StatusMessage = "Your potions is done"
            };


            _potionRepository.GetPotionByProperty(p => p.Id == potionId).ReturnsForAnyArgs(potion);
            _ingredientRepository.GetByProperty(i => i.Name == ingredientData.Name).ReturnsForAnyArgs(newIngredient);
            _recipeRepository.GetAllRecipes().Returns(recipes);

            // Act
            var result = await _sut.AddIngredientToPotion(potionId, ingredientData);

            // Assert 
            Assert.Equal(status.Status, result.Status);
            Assert.Equal(status.StatusMessage, result.StatusMessage);
            Assert.True(result.Potion.Ingredients.Count == 5);
            Assert.Equal(ingredientData.Name, result.Potion.Ingredients[4].Name);
            Assert.Contains(newIngredient, result.Potion.Ingredients);
            Assert.Equal(BrewingStatus.Discovery, result.Potion.BrewingStatus);
            Assert.Equal($"{potion.Maker.Name}'s Discovery #1", result.Potion.Recipe.Name);
            Assert.Equal(potion.Maker.Name, result.Potion.Recipe.Maker.Name);
        }
    }
}
