import {useState} from "react";
import AddRoomForm from "../components/AddRoomForm";

const AddRoom = () => {
    const [formInputs, setFormInputs] = useState({
        roomNumber: "",
        capacity: 0
    })
    const [status, setStatus] = useState("add")

    const handleSubmit = async (e) => {
        e.preventDefault();
        await addRoom()
    }

    const addRoom = async () => {
        const data = {
            number: parseInt(formInputs.roomNumber),
            capacity: parseInt(formInputs.capacity)
        }
        await fetch('rooms', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
        setFormInputs({
            roomNumber: "",
            capacity: 0
        })
        setStatus("success")
    }

    const handleInputChange = (e) => {
        const value = e.target.value;
        setFormInputs({
            ...formInputs, [e.target.name]: value
        });
    }

    const addAnother = () => {
        setStatus("add")
    }

    if (status === "add")
    {
        return <AddRoomForm formInputs={formInputs} handleSubmit={handleSubmit} handleInputChange={handleInputChange} />
    }
    else
    {
        return (
            <div className="d-flex align-items-center flex-column mt-5">
                <h3 className="text-center">Room added successfully!</h3>
                <button className="btn btn-light mt-4" onClick={addAnother}>Add another room</button>
            </div>
        )
    }
};

export default AddRoom;
