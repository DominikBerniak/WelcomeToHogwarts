using Microsoft.AspNetCore.Mvc;
using WelcomeToHogwarts.Persistance;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Services.Interfaces;

namespace WelcomeToHogwarts.Controllers
{
    [ApiController, Route("/rooms")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Room>>> GetAllRooms()
        {
            var rooms = await _service.GetAllRooms();
            if (rooms.Count == 0)
            {
                return NotFound("No rooms in Hogwarts");
            }
            return Ok(rooms);
        }

        [HttpPost]
        public async Task<ActionResult<Room>> AddRoom(Room room)
        {
            var createdRoom = await _service.AddRoom(room);
            return Created($"/room/{createdRoom.Id}", createdRoom);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoomById(Guid id)
        {
            var room = await _service.GetRoomById(id);
            if (room is null)
            {
                return NotFound("No such room");
            }
            return Ok(room);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomById(Guid id)
        {
            await _service.DeleteRoomById(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<Room>> GetRoomByNumber([FromQuery] int roomNumber)
        {
            var room = await _service.GetRoomByNumber(roomNumber);
            if (room is null)
            {
                return NotFound("No such room");
            }
            return Ok(room);
        }

        [HttpGet("rat-owners")]
        public async Task<ActionResult<List<Room>>> GetRoomsForRatOwners()
        {
            var rooms = await _service.GetRoomsForRatOwners();
            if (rooms.Count == 0)
            {
                return NotFound("No such rooms");
            }
            return Ok(rooms);
        }

        [HttpGet("available")]
        public async Task<ActionResult<List<Room>>> GetAvailableRooms()
        {
            var rooms = await _service.GetAvailableRooms();
            if (rooms.Count == 0)
            {
                return NotFound("No such rooms");
            }
            return Ok(rooms);
        }
    }
}
