using WelcomeToHogwarts.Persistance.DbSetup;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Repositories.Implementations;
using WelcomeToHogwarts.Repositories.Interfaces;
using WelcomeToHogwarts.Services.Implementations;
using WelcomeToHogwarts.Services.Interfaces;

namespace WelcomeToHogwarts
{
    public static class ConfigureStartup
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IPotionService, PotionService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IGenericRepository<Potion>, GenericRepository<Potion>>();
            services.AddScoped<IGenericRepository<Recipe>, GenericRepository<Recipe>>();
            services.AddScoped<IGenericRepository<Room>, GenericRepository<Room>>();
            services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
            services.AddScoped<IGenericRepository<Ingredient>, GenericRepository<Ingredient>>();
            services.AddScoped<IPotionRepository,PotionRepository>();
            services.AddScoped<IRecipeRepository,RecipeRepository>();
            services.AddScoped<IRoomRepository,RoomRepository>();
        }
    }
}
