import {useState} from "react";
import AddStudentForm from "../components/AddStudentForm";


const AddStudent = () => {
    const [formInputs, setFormInputs] = useState({
        name: "",
        houseType: 0,
        petType: 0
    })
    const [status, setStatus] = useState("add")

    const handleSubmit = async (e) => {
        e.preventDefault();
        await addStudent()
    }
    const addStudent = async () => {
        const data = {
            name: formInputs.name,
            houseType: parseInt(formInputs.houseType),
            petType: parseInt(formInputs.petType)
        }
        await fetch('students', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
        setFormInputs({
            name: "",
            houseType: 0,
            petType: 0
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
        return <AddStudentForm formInputs={formInputs} handleSubmit={handleSubmit} handleInputChange={handleInputChange} />
    }
    else
    {
        return (
            <div className="d-flex align-items-center flex-column mt-5">
                <h3 className="text-center">Student added successfully!</h3>
                <button className="btn btn-light mt-4" onClick={addAnother}>Add another student</button>
            </div>
        )
    }
};

export default AddStudent;
