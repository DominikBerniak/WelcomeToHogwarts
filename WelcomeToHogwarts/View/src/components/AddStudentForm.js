
const AddStudentForm = ({handleSubmit, formInputs, handleInputChange}) => {
    return (
        <div>
            <h2 className="text-center mt-5">Add a student</h2>
            <form className="d-flex flex-column mt-4 align-items-center" onSubmit={handleSubmit}>
                <label className="w-15">
                    Name:
                    <input type="text" className="form-control" value={formInputs.name} name="name"
                           placeholder="Name" onChange={handleInputChange}/>
                </label>
                <label className="w-15">
                    House Type:
                    <select value={formInputs.houseType} onChange={handleInputChange} className="form-select" name="houseType">
                        <option value="0">Gryffindor</option>
                        <option value="1">Hufflepuff</option>
                        <option value="2">Ravenclaw</option>
                        <option value="3">Slytherin</option>
                    </select>
                </label>
                <label className="w-15">
                    Pet Type:
                    <select value={formInputs.petType} onChange={handleInputChange} className="form-select" name="petType">
                        <option value="0">No pet</option>
                        <option value="1">Cat</option>
                        <option value="2">Rat</option>
                        <option value="3">Owl</option>
                    </select>
                </label>
                <button type="submit" className="btn btn-light">Add</button>
            </form>
        </div>
    );
};

export default AddStudentForm;