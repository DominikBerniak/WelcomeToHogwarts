using Microsoft.EntityFrameworkCore;
using WelcomeToHogwarts.Persistance.DbSetup;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Persistance.Enums;

namespace WelcomeToHogwarts.Persistance
{
    public class HogwartsContext : DbContext
    {
        public const int MaxIngredientsForPotions = 5;

        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.SeedData();
        }

        public DbSet<Room> Room { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Potion> Potion { get; set; }

        public async Task AddRoom(Room room)
        {
            await Room.AddAsync(room);
            await SaveChangesAsync();
        }

        public async Task<Room> GetRoom(Guid roomId)
        {
            return await Room.Where(room => room.Id == roomId)
                .Include(room => room.Residents).FirstOrDefaultAsync();
        }

        public async Task<List<Room>> GetAllRooms()
        {
            var rooms = await Room
                .Include(room => room.Residents)
                .ToListAsync();
            return rooms;
        }

        public async Task UpdateRoom(Room updatedRoom)
        {
            Room.Update(updatedRoom);
            await SaveChangesAsync();
        }

        public async Task DeleteRoom(Guid id)
        {
            var roomToDelete = await Room.Where(room => room.Id == id)
                .Include(room => room.Residents).FirstOrDefaultAsync();
            if (roomToDelete.Residents.Count > 0)
            {
                foreach (var student in roomToDelete.Residents)
                {
                    student.RoomId = null;
                    Student.Update(student);
                }
            }
            Room.Remove(roomToDelete);
            await SaveChangesAsync();
        }

        public async Task<Room> GetRoomByNumber(int roomNumber)
        {
            return await Room.Where(room => room.Number == roomNumber)
                .Include(room => room.Residents)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await Room.Where(room => room.Residents.All(resident => resident.PetType != PetType.Cat && resident.PetType != PetType.Owl))
                .Include(room => room.Residents)
                .ToListAsync();
        }
        public async Task<List<Room>> GetAvailableRooms()
        {
            return await Room.Where(room => room.Residents.Count < room.Capacity)
                .Include(room => room.Residents)
                .ToListAsync();
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await Student.ToListAsync();
        }

        public async Task<Student> GetStudentByName(string name)
        {
            return await Student.Where(student => student.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task AddStudent(Student student)
        {
            await Student.AddAsync(student);
            await SaveChangesAsync();
        }

        public async Task UpdateStudent(Student student)
        {
            Student.Update(student);
            await SaveChangesAsync();
        }
    }
}
