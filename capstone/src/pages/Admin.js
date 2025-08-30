import React, { useState, useEffect } from 'react';
import { blogPostService } from '../services/blogPosts';
import { authService } from '../services/auth';
 
const Admin = () => {
  const [pendingPosts, setPendingPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const user = authService.getCurrentUser();
 
  useEffect(() => {
    const fetchPendingPosts = async () => {
      try {
        const data = await blogPostService.getPending();
        setPendingPosts(data);
      } catch (error) {
        console.error('Error fetching pending posts:', error);
      } finally {
        setLoading(false);
      }
    };
 
    // Only fetch if user is admin
    if (user?.role === 'Admin') {
      fetchPendingPosts();
    } else {
      setLoading(false);
    }
  }, [user]);
 
  const handleApprove = async (id) => {
    try {
      await blogPostService.approve(id);
      setPendingPosts(pendingPosts.filter(post => post.id !== id));
    } catch (error) {
      console.error('Error approving post:', error);
    }
  };
 
  const handleReject = async (id) => {
    try {
      await blogPostService.reject(id);
      setPendingPosts(pendingPosts.filter(post => post.id !== id));
    } catch (error) {
      console.error('Error rejecting post:', error);
    }
  };
 
  // Check if user is admin - moved outside of useEffect
  if (user?.role !== 'Admin') {
    return (
      <div className="container mt-4">
        <div className="alert alert-danger">Access denied. Admin privileges required.</div>
      </div>
    );
  }
 
  if (loading) return <div className="container mt-4">Loading...</div>;
 
  return (
    <div className="container mt-4">
      <h1>Admin Dashboard</h1>
      <h2>Pending Posts for Review</h2>
     
      {pendingPosts.length === 0 ? (
        <div className="alert alert-info">No pending posts to review.</div>
      ) : (
        <div className="table-responsive">
          <table className="table table-striped">
            <thead>
              <tr>
                <th>Title</th>
                <th>Author</th>
                <th>Submitted</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {pendingPosts.map(post => (
                <tr key={post.id}>
                  <td>{post.title}</td>
                  <td>{post.author.username}</td>
                  <td>{new Date(post.createdAt).toLocaleDateString()}</td>
                  <td>
                    <button
                      onClick={() => handleApprove(post.id)}
                      className="btn btn-sm btn-success me-2"
                    >
                      Approve
                    </button>
                    <button
                      onClick={() => handleReject(post.id)}
                      className="btn btn-sm btn-danger"
                    >
                      Reject
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
 
export default Admin;