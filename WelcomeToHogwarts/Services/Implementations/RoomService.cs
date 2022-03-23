using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Persistance.Enums;
using WelcomeToHogwarts.Repositories.Interfaces;
using WelcomeToHogwarts.Services.Interfaces;

namespace WelcomeToHogwarts.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<Room> AddRoom(Room room)
        {
            return await _roomRepository.Add(room);
        }

        public async Task DeleteRoomById(Guid id)
        {
            var roomToDelete = await _roomRepository.GetRoomByProperty(r => r.Id == id);
            if (roomToDelete != null)
            {
                foreach (var resident in roomToDelete.Residents)
                {
                    resident.RoomId = null;
                }
                await _roomRepository.Delete(roomToDelete);
            }
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _roomRepository.GetAllRooms();
        }

        public async Task<List<Room>> GetAvailableRooms()
        {
            return await _roomRepository.GetAllRoomsByProperty(room => room.Residents.Count < room.Capacity);
        }

        public async Task<Room> GetRoomById(Guid id)
        {
            return await _roomRepository.GetRoomByProperty(r => r.Id == id);
        }

        public async Task<Room> GetRoomByNumber(int roomNumber)
        {
            return await _roomRepository.GetRoomByProperty(r => r.Number == roomNumber);
        }

        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await _roomRepository.GetAllRoomsByProperty(room => room.Residents.All(resident => resident.PetType != PetType.Cat && resident.PetType != PetType.Owl));
        }
    }
}
