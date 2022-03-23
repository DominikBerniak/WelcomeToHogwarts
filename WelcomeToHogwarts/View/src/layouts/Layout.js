import { Outlet, Link } from "react-router-dom";

const Layout = () => {
    return (
        <div>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <Link className="navbar-brand px-4" to="/">Hogwarts Houses</Link>
                <button className="navbar-toggler" type="button" data-toggle="collapse"
                        data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false"
                        aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNavAltMarkup">
                    <div className="navbar-nav">
                        <Link className="nav-item nav-link" to="/">Home</Link>
                        <Link className="nav-item nav-link" to="/rooms">Rooms</Link>
                        <Link className="nav-item nav-link" to="/room-search">Find room</Link>
                        <Link className="nav-item nav-link" to="/room-add">Add room</Link>
                        <Link className="nav-item nav-link" to="/students">Students</Link>
                        <Link className="nav-item nav-link" to="/student-add">Add student</Link>
                        <Link className="nav-item nav-link" to="/assign-student">Assign student to room</Link>
                        <Link className="nav-item nav-link" to="/potions">Potions</Link>
                        <Link className="nav-item nav-link" to="/brew">Brew a Potion</Link>
                        <Link className="nav-item nav-link" to="/find-potion">Find a Potion</Link>
                        <Link className="nav-item nav-link" to="/ingredients">Ingredients</Link>
                    </div>
                </div>
            </nav>

            <Outlet />
        </div>
    );
};

export default Layout;