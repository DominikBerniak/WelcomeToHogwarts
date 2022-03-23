import React from 'react';

const AssignForm = ({formInputs, handleSubmit, handleInputChange}) => {
    return (
        <div>
            <h2 className="text-center mt-5">Assign student to a room</h2>
            <form className="d-flex flex-column mt-4 align-items-center" onSubmit={handleSubmit}>
                <label className="w-15">
                    Student Name:
                    <input type="text" className="form-control" value={formInputs.name} name="name"
                           placeholder="Name" onChange={handleInputChange}/>
                </label>
                <label className="w-15">
                    Room Number:
                    <input type="text" className="form-control" value={formInputs.roomNumber} name="roomNumber"
                           placeholder="Room Number" onChange={handleInputChange}/>
                </label>
                <button type="submit" className="btn btn-light">Assign</button>
            </form>
        </div>
    );
};

export default AssignForm;