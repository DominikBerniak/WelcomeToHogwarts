
const SearchForm = ({roomNumber, handleSubmit, handleInputChange}) => {
    return (
        <div>
            <h2 className="text-center mt-5">Search for room</h2>
            <form className="form-inline d-flex justify-content-center mt-4" onSubmit={handleSubmit}>
                <div className="form-group mx-sm-3 mb-2">
                    <input type="text" className="form-control" value={roomNumber} placeholder="Room Number" onChange={handleInputChange}/>
                </div>
                <button type="submit" className="btn btn-light mb-2">Search</button>
            </form>
        </div>
    );
};

export default SearchForm;