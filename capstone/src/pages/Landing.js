import React from 'react';
import { useNavigate } from 'react-router-dom';
import './Landing.css';

const Landing = () => {
  const navigate = useNavigate();

  return (
    <div className="landing-container">
      <div className="landing-content">
        <h1 className="landing-title">Welcome to BlogCMS</h1>
        <p className="landing-subtitle">Share your thoughts with the world</p>
        
        <div className="landing-buttons">
          <button 
            className="btn btn-primary btn-lg landing-btn"
            onClick={() => navigate('/home')}
          >
            Guest User
          </button>
          
          <button 
            className="btn btn-success btn-lg landing-btn"
            onClick={() => navigate('/register')}
          >
            Register
          </button>
          
          <button 
            className="btn btn-outline-primary btn-lg landing-btn"
            onClick={() => navigate('/login')}
          >
            Login
          </button>
        </div>

        <div className="landing-features mt-5">
          <h3>Features:</h3>
          <ul className="list-unstyled">
            <li>✓ Read blog posts as a guest</li>
            <li>✓ Create and edit your own posts</li>
            <li>✓ Rich text editor for formatting</li>
            <li>✓ Admin moderation system</li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Landing;