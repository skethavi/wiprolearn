// src/pages/Home.js (Guest User Page)
import React, { useState, useEffect } from 'react';
import { blogPostService } from '../services/blogPosts';
import { authService } from '../services/auth';
import { Link } from 'react-router-dom';
import { Helmet } from 'react-helmet-async';

const Home = () => {
  const [posts, setPosts] = useState([]);
  const [filteredPosts, setFilteredPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const user = authService.getCurrentUser();

  useEffect(() => {
    const fetchPosts = async () => {
      try {
        const data = await blogPostService.getAll();
        setPosts(data);
        setFilteredPosts(data);
      } catch (error) {
        console.error('Error fetching posts:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchPosts();
  }, []);

  // Search functionality
  useEffect(() => {
    const results = posts.filter(post =>
      post.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
      post.excerpt.toLowerCase().includes(searchTerm.toLowerCase()) ||
      post.content.toLowerCase().includes(searchTerm.toLowerCase()) ||
      (post.tags && post.tags.toLowerCase().includes(searchTerm.toLowerCase())) ||
      (post.author && post.author.username.toLowerCase().includes(searchTerm.toLowerCase()))
    );
    setFilteredPosts(results);
  }, [searchTerm, posts]);

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const clearSearch = () => {
    setSearchTerm('');
  };

  if (loading) return <div className="container mt-4">Loading...</div>;

  return (
    <>
      <Helmet>
        <title>BlogCMS - Read Amazing Blog Posts</title>
        <meta name="description" content="Discover and read amazing blog posts on technology, lifestyle, travel, food and more. Join our community of readers and writers." />
        <meta name="keywords" content="blog, articles, posts, technology, lifestyle, travel, food, reading, content" />
        <meta property="og:title" content="BlogCMS - Read Amazing Blog Posts" />
        <meta property="og:description" content="Discover and read amazing blog posts on various topics." />
        <meta property="og:type" content="website" />
        <link rel="canonical" href="https://yourdomain.com/" />
      </Helmet>

      <div className="container mt-4">
        {/* Show message for guest users */}
        {!user && (
          <div className="alert alert-info text-center">
            <h4>Do you want to write a post? Make sure to register first.</h4>
            <div className="mt-2">
              <Link to="/register" className="btn btn-primary me-2">Register</Link>
              <Link to="/login" className="btn btn-outline-primary">Login</Link>
            </div>
          </div>
        )}

        {/* Search Bar */}
        <div className="row mb-4">
          <div className="col-md-8 mx-auto">
            <div className="input-group">
              <input
                type="text"
                className="form-control"
                placeholder="Search posts by title, content, tags, or author..."
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
              <button className="btn btn-primary" type="button">
                <i className="bi bi-search"></i> Search
              </button>
            </div>
            {searchTerm && (
              <div className="mt-2 text-muted">
                Showing {filteredPosts.length} of {posts.length} posts
                {filteredPosts.length === 0 && ' - No related posts were discovered.'}
              </div>
            )}
          </div>
        </div>

        <h1>Latest Blog Posts</h1>
        {filteredPosts.length === 0 ? (
          <div className="text-center py-5">
            <h3>No blog posts available.</h3>
            {searchTerm && (
              <button className="btn btn-outline-primary mt-2" onClick={clearSearch}>
                Clear search
              </button>
            )}
          </div>
        ) : (
          <div className="row">
            {filteredPosts.map(post => (
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
                    <h2 className="card-title h5">{post.title}</h2>
                    <p className="card-text">{post.excerpt}</p>
                    <Link to={`/post/${post.id}`} className="btn btn-primary">Read More</Link>
                  </div>
                  <div className="card-footer text-muted">
                    Posted by {post.author?.username} on {new Date(post.publishedAt).toLocaleDateString()}
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </>
  );
};

export default Home;

