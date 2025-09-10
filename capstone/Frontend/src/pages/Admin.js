import React, { useState, useEffect } from 'react';
import { blogPostService } from '../services/blogPosts';
import { authService } from '../services/auth';
import { Helmet } from 'react-helmet-async';

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

  if (user?.role !== 'Admin') {
    return (
      <>
        <Helmet>
          <title>Access Denied - BlogCMS Admin</title>
          <meta name="robots" content="noindex, nofollow" />
        </Helmet>
        <div className="container mt-4">
          <div className="alert alert-danger">Access denied. Admin privileges required.</div>
        </div>
      </>
    );
  }

  if (loading) return <div className="container mt-4">Loading...</div>;

  return (
    <>
      <Helmet>
        <title>Admin Dashboard - BlogCMS Content Moderation</title>
        <meta name="description" content="Admin dashboard for content moderation, user management, and platform administration. Review pending posts and manage blog content." />
        <meta name="keywords" content="admin, dashboard, content moderation, user management, platform administration, blog management" />
        <meta name="robots" content="noindex, nofollow" />
        <meta property="og:title" content="Admin Dashboard - BlogCMS" />
        <meta property="og:description" content="Admin dashboard for content moderation and management." />
        <meta property="og:type" content="website" />
      </Helmet>

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
                  <th>Content</th>
                  <th>Submitted</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {pendingPosts.map(post => (
                  <tr key={post.id}>
                    <td>{post.title}</td>
                    <td>{post.author.username}</td>
                    <td>{post.content}</td>
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
    </>
  );
};

export default Admin;