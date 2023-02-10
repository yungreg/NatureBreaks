import { Link, useNavigate } from "react-router-dom";
import "./NavBar.css";

export const NavBar1 = () => {
  const navigate = useNavigate();

  return (
    <ul className="navbar_list">
      <li className="navbar__item active">
        <Link className="navbar__link" to="/login">
          Log In
        </Link>
      </li>
      <li className="navbar__item active">
        <Link className="navbar__link" to="/favoritebreaks">
          Favorite Breaks
        </Link>
      </li>
      <li className="navbar__item active">
        <Link className="navbar__link" to="/breaks">
          Break Search
        </Link>
      </li>
      {localStorage.getItem("naturebreaks_user") ? (
        <button className="navbar__item_navbar__logout">
          <li>
            <Link
              className="navbar__link"
              to=""
              onClick={() => {
                localStorage.removeItem("naturebreaks_user");
                navigate("/login");
              }}
            >
              Logout
            </Link>
          </li>
        </button>
      ) : (
        ""
      )}
    </ul>
  );
};
