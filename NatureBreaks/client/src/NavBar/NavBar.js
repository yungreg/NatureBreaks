//^below is all my previous code. refactor this with what you copied.
// import { Link, useNavigate } from "react-router-dom";
// import "./NavBar.css";

// export const NavBar1 = () => {
//   const navigate = useNavigate();

//   return (
//     <ul className="navbar_list">
//       <li className="navbar__item active">
//         <Link className="navbar__link" to="/login">
//           Log In
//         </Link>
//       </li>
//       <li className="navbar__item active">
//         <Link className="navbar__link" to="/favoritebreaks">
//           Favorite Breaks
//         </Link>
//       </li>
//       <li className="navbar__item active">
//         <Link className="navbar__link" to="/breaks">
//           Break Search
//         </Link>
//       </li>
//       {localStorage.getItem("naturebreaks_user") ? (
//         <button className="navbar__item_navbar__logout">
//           <li>
//             <Link
//               className="navbar__link"
//               to=""
//               onClick={() => {
//                 localStorage.removeItem("naturebreaks_user");
//                 navigate("/login");
//               }}
//             >
//               Logout
//             </Link>
//           </li>
//         </button>
//       ) : (
//         ""
//       )}
//     </ul>
//   );
// };

import React, { useState } from "react";
import { NavLink as RRNavLink } from "react-router-dom";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
} from "reactstrap";
import { logout } from "../modules/authManager";

export default function Header({ isLoggedIn, userProfile }) {
  const [isOpen, setIsOpen] = useState(false);
  const toggle = () => setIsOpen(!isOpen);

  return (
    <div>
      <Navbar color="dark" dark expand="md">
        <NavbarBrand tag={RRNavLink} to="/">
          Grace Hopper Wisdom
        </NavbarBrand>
        <NavbarToggler onClick={toggle} />
        <Collapse isOpen={isOpen} navbar>
          <Nav className="mr-auto" navbar>
            {isLoggedIn && (
              <>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/add">
                    Add Quote
                  </NavLink>
                </NavItem>
                <NavItem>
                  <a
                    aria-current="page"
                    className="nav-link"
                    style={{ cursor: "pointer" }}
                    onClick={logout}
                  >
                    Logout
                  </a>
                </NavItem>
              </>
            )}
            {!isLoggedIn && (
              <>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/login">
                    Login
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/register">
                    Register
                  </NavLink>
                </NavItem>
              </>
            )}
          </Nav>

          <Nav navbar>
            <NavItem>
              <a
                aria-current="page"
                className="nav-link"
                href="https://www.youtube.com/shorts/DiMaFbpKAjI"
                target="_new"
              >
                THis is proof this worked
              </a>
            </NavItem>
            {userProfile && (
              <NavItem>
                <NavLink>Welcome, {userProfile.name}!</NavLink>
              </NavItem>
            )}
          </Nav>
        </Collapse>
      </Navbar>
    </div>
  );
}
