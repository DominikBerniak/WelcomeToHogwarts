import {useState, useEffect} from "react";
import Spinner from "../components/Spinner";

const Students = () => {
    const [students, setStudents] = useState([]);
    const [isDataFetched, setIsDataFetched] = useState(false);

    useEffect(() => {
        const getStudents = async () => {
            const students = await fetchData();
            setStudents(students)
            setIsDataFetched(true);
        }
        getStudents();
    }, [])
    const fetchData = async () => {
        const response = await fetch("students");
        return await response.json();
    }

    const getPetName = (petNum) => {
        switch (petNum) {
            case 0:
                return "No pet";
            case 1:
                return "Cat";
            case 2:
                return "Rat";
            case 3:
                return "Owl"
        }
    }

    const getHouseName = (houseNum) => {
        switch (houseNum) {
            case 0:
                return "Gryffindor";
            case 1:
                return "Hufflepuff";
            case 2:
                return "Ravenclaw";
            case 3:
                return "Slytherin"
        }
    }

    return (
        <div>
            <h1 className="text-center mt-5">Hogwarts students</h1>
            <table className="table table-hover w-40 mt-4 mx-auto">
                <thead>
                <tr>
                    <th className="w-10 text-center">#</th>
                    <th>Name</th>
                    <th>House Type</th>
                    <th>Pet Type</th>
                </tr>
                </thead>
                <tbody>
                {isDataFetched &&
                    students.map((student, index) =>
                        <tr key={student.id}>
                            <td className="text-center align-middle">{index + 1}</td>
                            <td className="align-middle">{student.name}</td>
                            <td className="align-middle">{getHouseName(student.houseType)}</td>
                            <td className="align-middle">{getPetName(student.petType)}</td>
                        </tr>
                    )}
                </tbody>
            </table>
            {!isDataFetched &&
                <Spinner />
            }
        </div>
    );
};

export default Students;
