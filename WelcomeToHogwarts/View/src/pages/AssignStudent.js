import {useState} from "react";
import AssignForm from "../components/AssignForm";

const AssignStudent = () => {
    const [formInputs, setFormInputs] = useState({
        name: "",
        roomNumber: ""
    })

    const [status, setStatus] = useState("add")

    const handleSubmit = async (e) => {
        e.preventDefault();
        await assignStudent()
    }
    const assignStudent = async () => {
        const data = {
            studentName: formInputs.name,
            roomNumber: parseInt(formInputs.roomNumber)
        }
        await fetch('students/assign-student', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify(data),
        })
            .then(response => response.json())
            .then(data => {
                setFormInputs({
                    name: "",
                    roomNumber: ""
                });
                if (data === "Room full") {
                    setStatus("room full");
                } else if (data === "No such student") {
                    setStatus("no student");
                } else if (data === "Not such room") {
                    setStatus("no room");
                } else {
                    setStatus("success");
                }
            })
    }
    const handleInputChange = (e) => {
        const value = e.target.value;
        setFormInputs({
            ...formInputs, [e.target.name]: value
        });
    }
    const assignAnother = () => {
        setStatus("add")
    }
    if (status === "add") {
        return <AssignForm formInputs={formInputs} handleSubmit={handleSubmit} handleInputChange={handleInputChange}/>
    } else if (status === "no room") {
        return (
            <div className="d-flex align-items-center flex-column mt-5">
                <h3 className="text-center">No such room found!</h3>
                <button className="btn mt-4" onClick={assignAnother}>Try another room</button>
            </div>
        )
    } else if (status === "no student") {
        return (
            <div className="d-flex align-items-center flex-column mt-5">
                <h3 className="text-center">No such student found!</h3>
                <button className="btn mt-4" onClick={assignAnother}>Try another room</button>
            </div>
        )
    } else if (status === "room full") {
        return (
            <div className="d-flex align-items-center flex-column mt-5">
                <h3 className="text-center">This room is already full</h3>
                <button className="btn mt-4" onClick={assignAnother}>Try another room</button>
            </div>
        )
    } else {
        return (
            <div className="d-flex align-items-center flex-column mt-5">
                <h3 className="text-center">Student assigned to a room successfully!</h3>
                <button className="btn mt-4" onClick={assignAnother}>Assign another student</button>
            </div>
        )
    }
};

export default AssignStudent;
