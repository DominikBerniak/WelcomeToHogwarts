import {Route, Routes} from "react-router-dom";
import "./styles/App.css"
import Layout from "./layouts/Layout";
import Rooms from "./pages/Rooms";
import Home from "./pages/Home";
import RoomSearch from "./pages/RoomSearch"
import AddRoom from "./pages/AddRoom"
import AddStudent from "./pages/AddStudent";
import Students from "./pages/Students";
import AssignStudent from "./pages/AssignStudent";
import Error from "./pages/Error";
import Ingredients from "./pages/Ingredients";
import Potions from "./pages/Potions";
import BrewPotion from "./pages/BrewPotion";
import FindPotion from "./pages/FindPotion";

function App() {
    return (
        <Routes>
            <Route path="/" element={<Layout/>}>
                <Route index element={<Home/>}/>
                <Route path="rooms" element={<Rooms/>}/>
                <Route path="room-search" element={<RoomSearch/>}/>
                <Route path="room-add" element={<AddRoom/>}/>
                <Route path="students" element={<Students/>}/>
                <Route path="student-add" element={<AddStudent/>}/>
                <Route path="assign-student" element={<AssignStudent/>}/>
                <Route path="ingredients" element={<Ingredients/>}/>
                <Route path="potions" element={<Potions/>}/>
                <Route path="brew" element={<BrewPotion/>}/>
                <Route path="find-potion" element={<FindPotion/>}/>
                <Route path="*" element={<Error/>}/>
            </Route>
        </Routes>
    );
}

export default App;
