using Microsoft.EntityFrameworkCore;

namespace WelcomeToHogwarts.Persistance.DbSetup
{
    public class DbInitializer : IDbInitializer
    {
        private readonly HogwartsContext _context;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(HogwartsContext context, ILogger<DbInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void Initialize()
        {
            _logger.LogInformation("pending migrations: " + _context.Database.GetPendingMigrations().Count().ToString());
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception e)
            {

                _logger.LogError(e.ToString());
            }
        }
    }
}
