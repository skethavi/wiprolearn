// src/pages/Dashboard.js
import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { blogPostService } from '../services/blogPosts';
import { authService } from '../services/auth';

const Dashboard = () => {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const user = authService.getCurrentUser();

  useEffect(() => {
    const fetchUserPosts = async () => {
      try {
        const data = await blogPostService.getUserPosts();
        setPosts(data);
      } catch (error) {
        console.error('Error fetching user posts:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchUserPosts();
  }, []);

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this post?')) {
      try {
        await blogPostService.delete(id);
        setPosts(posts.filter(post => post.id !== id));
      } catch (error) {
        console.error('Error deleting post:', error);
      }
    }
  };

  if (loading) return <div className="container mt-4">Loading...</div>;

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h1>My Blog Posts</h1>
        <Link to="/create-post" className="btn btn-primary">Create New Post</Link>
      </div>
      
      {posts.length === 0 ? (
        <div className="text-center py-5">
          <h3>You haven't created any blog posts yet.</h3>
          <Link to="/create-post" className="btn btn-primary mt-3">Create Your First Post</Link>
        </div>
      ) : (
        <div className="table-responsive">
          <table className="table table-striped">
            <thead>
              <tr>
                <th>Title</th>
                <th>Status</th>
                <th>Created</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {posts.map(post => (
                <tr key={post.id}>
                  <td>{post.title}</td>
                  <td>
                    <span className={`badge ${post.status === 'Published' ? 'bg-success' : 
                                     post.status === 'Pending' ? 'bg-warning' : 
                                     post.status === 'Rejected' ? 'bg-danger' : 'bg-secondary'}`}>
                      {post.status}
                    </span>
                  </td>
                  <td>{new Date(post.createdAt).toLocaleDateString()}</td>
                  <td>
                    <Link to={`/edit-post/${post.id}`} className="btn btn-sm btn-outline-primary me-2">Edit</Link>
                    <button 
                      onClick={() => handleDelete(post.id)} 
                      className="btn btn-sm btn-outline-danger"
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default Dashboard;