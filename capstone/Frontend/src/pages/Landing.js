import React from 'react';
import { useNavigate } from 'react-router-dom';
import './Landing.css';

const Landing = () => {
  const navigate = useNavigate();

  return (
    <div className="landing-container">
      <div className="landing-content">
        <h1 className="landing-title">Hello there, new blogger!</h1>
        <p className="landing-subtitle">Post your opinions here for everyone to see.</p>
        
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
            <li>📝 Write and Format Posts</li>
            <li>📅 Save Drafts & Schedule Posts</li>
            <li>🔍 Easy Navigation</li>
            <li>📚 Expert Articles & Blog Posts</li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Landing;