import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { authService } from '../services/auth';
 
const Header = () => {
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const navigate = useNavigate();
  const user = authService.getCurrentUser();
 
  const handleLogout = () => {
    authService.logout();
    navigate('/');
    window.location.reload();
  };
 
  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      <div className="container">
        <Link className="navbar-brand" to={user ? "/home" : "/"}>BloggerAndCms</Link>
       
        {user && (
          <>
            <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarNav">
              <ul className="navbar-nav me-auto">
                <li className="nav-item">
                  <Link className="nav-link" to="/home">Home</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/dashboard">Dashboard</Link>
                </li>
                {user.role === 'Admin' && (
                  <li className="nav-item">
                    <Link className="nav-link" to="/admin">Admin</Link>
                  </li>
                )}
              </ul>
              <ul className="navbar-nav">
                <li className="nav-item dropdown">
                  <a
                    className="nav-link dropdown-toggle"
                    href="#"
                    role="button"
                    onClick={() => setDropdownOpen(!dropdownOpen)}
                    style={{ cursor: 'pointer' }}
                  >
                    {user.username}
                  </a>
                  {dropdownOpen && (
                    <ul className="dropdown-menu show" style={{display: 'block', position: 'absolute', right: 0, left: 'auto'}}>
                      <li><Link className="dropdown-item" to="/profile" onClick={() => setDropdownOpen(false)}>Profile</Link></li>
                      <li><hr className="dropdown-divider" /></li>
                      <li>
                        <button className="dropdown-item" onClick={handleLogout} style={{ cursor: 'pointer' }}>
                          Logout
                        </button>
                      </li>
                    </ul>
                  )}
                </li>
              </ul>
            </div>
          </>
        )}
      </div>
    </nav>
  );
};
 
export default Header;