const Room = ({room}) => {
    if (room.id === undefined) {
        return null;
    }
    return (
        <table className="table table-hover w-40 mt-4 mx-auto">
            <thead>
            <tr>
                <th className="w-5 text-center">Id</th>
                <th>Residents</th>
                <th className="w-20 text-center">Room Capacity</th>
                <th className="w-8"></th>
            </tr>
            </thead>
            <tbody>
            <tr>
                <td className="text-center">{room.id}</td>
                <td>
                    {
                        room.residents.length > 0 ?
                        room.residents.map(resident =>
                            <div key={resident.id}>{resident.name}</div>
                    )
                    : <div>No residents</div>
                    }
                </td>
                <td className="text-center">{room.capacity}</td>
            </tr>
            </tbody>
        </table>
    );
};

export default Room;