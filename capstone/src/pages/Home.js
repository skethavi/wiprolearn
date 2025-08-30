import React, { useState, useEffect } from 'react';
import { blogPostService } from '../services/blogPosts';
import { authService } from '../services/auth';
import { Link, useNavigate } from 'react-router-dom';

const Home = () => {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const user = authService.getCurrentUser();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPosts = async () => {
      try {
        const data = await blogPostService.getAll();
        setPosts(data);
      } catch (error) {
        console.error('Error fetching posts:', error);
        setError('Failed to load posts. Please try again later.');
      } finally {
        setLoading(false);
      }
    };

    fetchPosts();
  }, []);

  if (loading) return <div className="container mt-4">Loading...</div>;
  if (error) return <div className="container mt-4 alert alert-danger">{error}</div>;

  return (
    <div className="container mt-4">
      {/* Show message for guest users */}
      {!user && (
        <div className="alert alert-info text-center">
          <h4>Want to create a post? Please register first.</h4>
          <div className="mt-2">
            <Link to="/register" className="btn btn-primary me-2">Register</Link>
            <Link to="/login" className="btn btn-outline-primary">Login</Link>
          </div>
        </div>
      )}

      <h1>Latest Blog Posts</h1>
      {posts.length === 0 ? (
        <div className="text-center py-5">
          <h3>No blog posts available yet.</h3>
          {user && (
            <Link to="/create-post" className="btn btn-primary mt-3">Create Your First Post</Link>
          )}
        </div>
      ) : (
        <div className="row">
          {posts.map(post => (
            <div key={post.id} className="col-md-6 mb-4">
              <div className="card h-100">
                {post.featuredImageUrl && (
                  <img 
                    src={post.featuredImageUrl} 
                    className="card-img-top" 
                    alt={post.title}
                    style={{ height: '200px', objectFit: 'cover' }}
                  />
                )}
                <div className="card-body">
                  <h5 className="card-title">{post.title}</h5>
                  <p className="card-text">{post.excerpt || 'No excerpt available.'}</p>
                  <Link to={`/post/${post.id}`} className="btn btn-primary">Read More</Link>
                </div>
                <div className="card-footer text-muted">
                  Posted by {post.author?.username || 'Unknown'} on {new Date(post.publishedAt).toLocaleDateString()}
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default Home;