using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WelcomeToHogwarts.Persistance;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Persistance.Enums;
using WelcomeToHogwarts.Repositories.Interfaces;

namespace WelcomeToHogwarts.Repositories.Implementations
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(HogwartsContext context) : base(context)
        {
        }

        public async Task<List<Room>> GetAllRoomsByProperty(System.Linq.Expressions.Expression<Func<Room, bool>> predicate)
        {
            return await _context.Room.Where(predicate)
                .Include(room => room.Residents)
                .ToListAsync();
        }
        public async Task<List<Room>> GetAllRooms()
        {
            return await _context.Room
                .Include(room => room.Residents)
                .ToListAsync();
        }

        public async Task<Room> GetRoomByProperty(Expression<Func<Room, bool>> predicate)
        {
            return await _context.Room.Where(predicate)
                .Include(room => room.Residents)
                .FirstOrDefaultAsync();
        }
    }
}
