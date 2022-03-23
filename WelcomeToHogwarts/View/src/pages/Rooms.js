import {useState, useEffect} from "react";
import Spinner from "../components/Spinner";

const Rooms = () => {
    const [rooms, setRooms] = useState([]);
    const [filterOption, setFilterOption] = useState("");
    const [isDataFetched, setIsDataFetched] = useState(false);

    useEffect(async () => {
            const roomsData = await fetchData();
            if(roomsData === "No rooms in Hogwarts" || roomsData === "No such rooms")
            {
                setRooms([]);
            }
            else
            {
                setRooms(roomsData);
            }
            setIsDataFetched(true);
    }, [filterOption])

    const fetchData = async () => {
        const response = await fetch(`rooms${filterOption}`);
        if(response.ok)
        {
            return await response.json();
        }
        return await response.text();
    }

    const deleteRoom = async (id) => {
        await fetch(`rooms/${id}`, {
            method: 'DELETE'
        })
        setRooms(rooms.filter(room => room.id !== id))
    }
    return (
        <div>
            <h1 className="text-center mt-5">Rooms</h1>
            <form className="d-flex justify-content-around mt-3 w-30 mx-auto">
                <label>
                    Show all rooms
                    <input className="align-middle ml-2" type="radio" checked={filterOption === ""}
                           onChange={() => setFilterOption("")}/>
                </label>
                <label>
                    Show only available rooms
                    <input className="align-middle ml-2" type="radio" checked={filterOption === "/available"}
                           value="available" onChange={() => setFilterOption("/available")}/>
                </label>
                <label>
                    Show only rat rooms
                    <input className="align-middle ml-2" type="radio" checked={filterOption === "/rat-owners"}
                           value="rat" onChange={() => setFilterOption("/rat-owners")}/>
                </label>
            </form>
            <table className="table table-hover w-40 mt-4 mx-auto">
                <thead>
                <tr>
                    <th className="w-10 text-center">#</th>
                    <th>Residents</th>
                    <th className="w-20 text-center">Room Capacity</th>
                    <th className="w-8"></th>
                </tr>
                </thead>
                {isDataFetched &&
                    rooms.length > 0 &&
                            <tbody>
                            {rooms.map(room =>
                                <tr key={room.id}>
                                    <td className="text-center align-middle">{room.number}</td>
                                    <td className="align-middle">
                                        {room.residents.length > 0 ?
                                            room.residents.map(resident =>
                                                <div key={resident.id}>{resident.name}</div>
                                            )
                                            : <div>No residents</div>
                                        }
                                    </td>
                                    <td className="text-center align-middle">{room.capacity}</td>
                                    <td>
                                        <button className="btn" onClick={() => deleteRoom(room.id)}>X</button>
                                    </td>
                                </tr>
                            )}
                            </tbody>
                }
            </table>
            {isDataFetched && rooms.length === 0 &&
                <h4 className="text-center">{filterOption === "" ? "No rooms added to Hogwarts yet." : "No rooms matching search criteria."}</h4>

            }
            {!isDataFetched &&
                <Spinner/>
            }
        </div>
    );
};

export default Rooms;
