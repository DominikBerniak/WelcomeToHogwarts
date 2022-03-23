using Microsoft.AspNetCore.Mvc;
using WelcomeToHogwarts.Persistance.Entities;

namespace WelcomeToHogwarts.Services.Interfaces
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRooms();
        Task<Room> AddRoom(Room room);
        Task<Room> GetRoomById(Guid id);
        Task DeleteRoomById(Guid id);
        Task<Room> GetRoomByNumber(int roomNumber);
        Task<List<Room>> GetRoomsForRatOwners();
        Task<List<Room>> GetAvailableRooms();
    }
}
