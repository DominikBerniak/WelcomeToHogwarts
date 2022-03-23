import {useState} from "react";
import SearchForm from "../components/SearchForm";
import Room from "../components/Room";

const RoomSearch = () => {

    const [roomNumber, setRoomNumber] = useState("");
    const [room, setRoom] = useState({});
    const [status, setStatus] = useState("Search for room by providing it's number")
    const handleInputChange = (e) => {
        setRoomNumber(e.target.value);
    }
    const handleSubmit = async (e) => {
        e.preventDefault();
        const roomData = await fetchRoom();
        if (roomData === "No such room")
        {
            setRoom({})
            setStatus("No such room found!")
            return;
        }
        setStatus("Search for another room!")
        setRoom(roomData)
    }

    const fetchRoom = async () => {
        const response = await fetch(`rooms/search?roomNumber=${roomNumber}`);
        if(response.ok)
        {
            return await response.json();
        }
        return await response.text();
    }

    return (
        <div>
            <SearchForm roomId={roomNumber} handleSubmit={handleSubmit} handleInputChange={handleInputChange}/>
            <Room room={room}/>
            <p className="text-center mt-2">{status}</p>
        </div>
    );
};

export default RoomSearch;
