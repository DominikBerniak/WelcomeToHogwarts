
const AddRoomForm = ({handleSubmit, formInputs, handleInputChange}) => {
    return (
        <div>
            <h2 className="text-center mt-5">Add a room</h2>
            <form className="d-flex flex-column mt-4 align-items-center" onSubmit={handleSubmit}>
                <label>
                    Room Number:
                    <input type="text" className="form-control" value={formInputs.roomNumber} name="roomNumber"
                           placeholder="Room Number" onChange={handleInputChange}/>
                </label>
                <label>
                    Room Capacity:
                    <input type="number" className="form-control" value={formInputs.capacity} name="capacity"
                           onChange={handleInputChange}/>
                </label>
                <button type="submit" className="btn btn-light">Add</button>
            </form>
        </div>
    );
};

export default AddRoomForm;