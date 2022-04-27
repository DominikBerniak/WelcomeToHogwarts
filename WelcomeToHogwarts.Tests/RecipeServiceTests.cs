using Xunit;
using NSubstitute;
using WelcomeToHogwarts.Services.Implementations;
using WelcomeToHogwarts.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using WelcomeToHogwarts.Persistance.DataTransferObjects;
using NSubstitute.ReturnsExtensions;
using WelcomeToHogwarts.Persistance.Entities;
using System.Collections.Generic;
using WelcomeToHogwarts.Repositories.Implementations;

namespace WelcomeToHogwarts.Tests
{
    public class RecipeServiceTests
    {
        private readonly RecipeService _sut;
        private readonly IRecipeRepository _recipeRepository = Substitute.For<IRecipeRepository>();
        private readonly IPotionRepository _potionRepository = Substitute.For<IPotionRepository>();

        public RecipeServiceTests()
        {
            _sut = new RecipeService(_recipeRepository, _potionRepository);
        }

        [Fact]
        public async Task GetMatchingRecipes_NotExistingPotionShouldFail()
        {
            // Arrange
            var status = new GetRecipesStatusDto
            {
                Status = Status.NotFound,
                StatusMessage = "No such potion",
            };
            var potionId = Guid.NewGuid();

            _potionRepository.GetPotionByProperty(p => p.Id == potionId).ReturnsNull();
            // Act
            var result = await _sut.GetMatchingRecipes(potionId);
            // Assert 
            Assert.Equal(status.Status, result.Status);
            Assert.Equal(status.StatusMessage, result.StatusMessage);
        }

        [Fact]
        public async Task GetMatchingRecipes_NoMatchingRecipesShouldFail()
        {
            // Arrange
            var status = new GetRecipesStatusDto
            {
                Status = Status.NotFound,
                StatusMessage = "No matching recipes found for the potion",
            };
            var potionId = Guid.NewGuid();
            var potion = new Potion
            {
                Id = potionId,
                Name = "Elixir",
                Maker = new Student(),
                Ingredients = new List<Ingredient>
                {
                    new Ingredient
                    {
                        Name = "Water"
                    }
                }
            };

            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Name = "Recipe",
                    Maker = new Student(),
                    MakerId = Guid.NewGuid(),
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient
                        {
                            Name = "Alcohol"
                        }
                    }
                }
            };

            _potionRepository.GetPotionByProperty(p => p.Id == potionId).ReturnsForAnyArgs(potion);
            _recipeRepository.GetAllRecipes().Returns(recipes);
            // Act
            var result = await _sut.GetMatchingRecipes(potionId);
            // Assert 
            Assert.Equal(status.Status, result.Status);
            Assert.Equal(status.StatusMessage, result.StatusMessage);
        }

        [Fact]
        public async Task GetMatchingRecipes_ShouldReturnMatchingRecipes()
        {
            // Arrange
            var ingredient = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Water"
            };
            var potionId = Guid.NewGuid();
            var potion = new Potion
            {
                Id = potionId,
                Name = "Elixir",
                Maker = new Student(),
                Ingredients = new List<Ingredient>
                {
                    ingredient
                }
            };

            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Name = "Recipe",
                    Maker = new Student(),
                    MakerId = Guid.NewGuid(),
                    Ingredients = new List<Ingredient>
                    {
                        ingredient
                    }
                }
            };

            var status = new GetRecipesStatusDto
            {
                Status = Status.Ok,
                Recipes = recipes
            };

            _potionRepository.GetPotionByProperty(p => p.Id == potionId).ReturnsForAnyArgs(potion);
            _recipeRepository.GetAllRecipes().Returns(recipes);
            // Act
            var result = await _sut.GetMatchingRecipes(potionId);
            // Assert 
            Assert.Equal(status.Status, result.Status);
            Assert.Equal(status.Recipes, result.Recipes);
        }
    }
}
