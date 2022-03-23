using System.Linq.Expressions;
using WelcomeToHogwarts.Persistance.Entities;

namespace WelcomeToHogwarts.Repositories.Interfaces
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<Room> GetRoomByProperty(Expression<Func<Room, bool>> predicate);
        Task<List<Room>> GetAllRoomsByProperty(Expression<Func<Room, bool>> predicate);
        Task<List<Room>> GetAllRooms();
    }
}
