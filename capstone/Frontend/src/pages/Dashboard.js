import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { blogPostService } from '../services/blogPosts';
import { authService } from '../services/auth';
import { Helmet } from 'react-helmet-async';

const Dashboard = () => {
  const [posts, setPosts] = useState([]);
  const [filteredPosts, setFilteredPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const user = authService.getCurrentUser();

  useEffect(() => {
    const fetchUserPosts = async () => {
      try {
        const data = await blogPostService.getUserPosts();
        setPosts(data);
        setFilteredPosts(data);
      } catch (error) {
        console.error('Error fetching user posts:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchUserPosts();
  }, []);

  // Search functionality
  useEffect(() => {
    const results = posts.filter(post =>
      post.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
      post.content.toLowerCase().includes(searchTerm.toLowerCase()) ||
      post.status.toLowerCase().includes(searchTerm.toLowerCase()) ||
      (post.tags && post.tags.toLowerCase().includes(searchTerm.toLowerCase()))
    );
    setFilteredPosts(results);
  }, [searchTerm, posts]);

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const clearSearch = () => {
    setSearchTerm('');
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you certain you want to delete this post?')) {
      try {
        await blogPostService.delete(id);
        const updatedPosts = posts.filter(post => post.id !== id);
        setPosts(updatedPosts);
        setFilteredPosts(updatedPosts);
      } catch (error) {
        console.error('Error deleting post:', error);
      }
    }
  };

  if (loading) return <div className="container mt-4">Loading...</div>;

  return (
    <>
      <Helmet>
        <title>My Dashboard - BlogCMS</title>
        <meta name="description" content="Manage your blog posts, create new content, and track your publishing activity on your personal dashboard." />
        <meta name="keywords" content="dashboard, manage posts, content management, blogging, user account" />
        <meta property="og:title" content="My Dashboard - Blogging" />
        <meta property="og:description" content="Manage your blogging posts and content." />
        <meta property="og:type" content="website" />
      </Helmet>

      <div className="container mt-4">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <h1>My Blog Posts</h1>
          <Link to="/create-post" className="btn btn-primary">Create New Post</Link>
        </div>

        {/* Search Bar */}
        <div className="row mb-4">
          <div className="col-md-6">
            <div className="input-group">
              <input
                type="text"
                className="form-control"
                placeholder="Search your posts..."
                value={searchTerm}
                onChange={handleSearch}
              />
              {searchTerm && (
                <button
                  className="btn btn-outline-secondary"
                  type="button"
                  onClick={clearSearch}
                >
                  Clear
                </button>
              )}
            </div>
            {searchTerm && (
              <div className="mt-2 text-muted">
                Showing {filteredPosts.length} of {posts.length} posts
              </div>
            )}
          </div>
        </div>
        
        {filteredPosts.length === 0 ? (
          <div className="text-center py-5">
            <h3>{searchTerm ? 'No posts match your search' : 'You haven\'t created any blog posts yet.'}</h3>
            {searchTerm ? (
              <button className="btn btn-outline-primary mt-2" onClick={clearSearch}>
                Clear search
              </button>
            ) : (
              <Link to="/create-post" className="btn btn-primary mt-3">Create Your First Post</Link>
            )}
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
                {filteredPosts.map(post => (
                  <tr key={post.id}>
                    <td>
                      <Link to={`/post/${post.id}`} className="text-decoration-none">
                        {post.title}
                      </Link>
                    </td>
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
    </>
  );
};

export default Dashboard;