import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { blogPostService } from '../services/blogPosts';
import { authService } from '../services/auth'; // Import your auth service

const CreateEditPost = () => {
  const [formData, setFormData] = useState({
    title: '',
    content: '',
    excerpt: '',
    slug: '',
    featuredImageUrl: '',
    status: 'Draft',
    metaTitle: '',
    metaDescription: '',
    tags: '',
    categoryIds: []
  });
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [categories, setCategories] = useState([]);
  const [userRole, setUserRole] = useState(''); // State to store user role
  const navigate = useNavigate();
  const { id } = useParams();
  const isEditing = !!id;

  useEffect(() => {
    // Get user role from auth service or localStorage
    const getUserRole = () => {
      const user = authService.getCurrentUser(); // Assuming you have this method
      if (user) {
        setUserRole(user.role);
      } else {
        // Fallback to localStorage if needed
        const storedUser = localStorage.getItem('user');
        if (storedUser) {
          const userData = JSON.parse(storedUser);
          setUserRole(userData.role);
        }
      }
    };

    getUserRole();

    // In a real app, you would fetch categories from an API
    setCategories([
      { id: 1, name: 'Technology' },
      { id: 2, name: 'Lifestyle' },
      { id: 3, name: 'Travel' },
      { id: 4, name: 'Food' },
      { id: 5, name: 'DIY & Crafts' },
      { id: 6, name: 'Health & Wellness' },
      { id: 7, name: 'Finance' },
      { id: 8, name: 'Education' },
      { id: 9, name: 'Entertainment' },
      { id: 10, name: 'Sports' },
      { id: 11, name: 'Other' }
    ]);

    if (isEditing) {
      const fetchPost = async () => {
        try {
          const post = await blogPostService.getById(id);
          setFormData({
            title: post.title,
            content: post.content,
            excerpt: post.excerpt || '',
            slug: post.slug,
            featuredImageUrl: post.featuredImageUrl || '',
            status: post.status,
            metaTitle: post.metaTitle || '',
            metaDescription: post.metaDescription || '',
            tags: post.tags || '',
            categoryIds: post.categories.map(c => c.id)
          });
        } catch (error) {
          console.error('Error fetching post:', error);
        }
      };
      fetchPost();
    }
  }, [id, isEditing]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value
    });
  };

  const handleCategoryChange = (e) => {
    const options = e.target.options;
    const selectedIds = [];
    for (let i = 0; i < options.length; i++) {
      if (options[i].selected) {
        selectedIds.push(parseInt(options[i].value));
      }
    }
    setFormData({
      ...formData,
      categoryIds: selectedIds
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      if (isEditing) {
        await blogPostService.update(id, formData);
      } else {
        await blogPostService.create(formData);
      }
      navigate('/dashboard');
    } catch (error) {
      setError(error.response?.data?.message || 'Error saving post');
    } finally {
      setLoading(false);
    }
  };

  const generateSlug = () => {
    const slug = formData.title
      .toLowerCase()
      .replace(/[^a-z0-9 -]/g, '')
      .replace(/\s+/g, '-')
      .replace(/-+/g, '-')
      .trim();
   
    setFormData({
      ...formData,
      slug
    });
  };

  // Render different status options based on user role
  const renderStatusOptions = () => {
    if (userRole === 'Admin') {
      return (
        <>
          <option value="Draft">Draft</option>
          <option value="Published">Publish Immediately</option>
          <option value="Pending">Submit for Review</option>
        </>
      );
    } else {
      // Regular user options
      return (
        <>
          <option value="Draft">Draft</option>
          <option value="Pending">Submit for Review</option>
        </>
      );
    }
  };

  return (
    <div className="container mt-4">
      <h1>{isEditing ? 'Edit' : 'Create'} Blog Post</h1>
     
      {error && <div className="alert alert-danger">{error}</div>}
     
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="title" className="form-label">Title</label>
          <input
            type="text"
            className="form-control"
            id="title"
            name="title"
            value={formData.title}
            onChange={handleChange}
            required
          />
        </div>
       
        <div className="mb-3">
          <label htmlFor="slug" className="form-label">Slug</label>
          <div className="input-group">
            <input
              type="text"
              className="form-control"
              id="slug"
              name="slug"
              value={formData.slug}
              onChange={handleChange}
              required
            />
            <button
              type="button"
              className="btn btn-outline-secondary"
              onClick={generateSlug}
            >
              Generate
            </button>
          </div>
        </div>
       
        <div className="mb-3">
          <label htmlFor="excerpt" className="form-label">Excerpt</label>
          <textarea
            className="form-control"
            id="excerpt"
            name="excerpt"
            rows="3"
            value={formData.excerpt}
            onChange={handleChange}
          ></textarea>
        </div>
       
        <div className="mb-3">
          <label htmlFor="content" className="form-label">Content</label>
          <textarea
            className="form-control"
            id="content"
            name="content"
            rows="10"
            value={formData.content}
            onChange={handleChange}
            required
          ></textarea>
        </div>
       
        <div className="mb-3">
          <label htmlFor="featuredImageUrl" className="form-label">Featured Image URL</label>
          <input
            type="url"
            className="form-control"
            id="featuredImageUrl"
            name="featuredImageUrl"
            value={formData.featuredImageUrl}
            onChange={handleChange}
          />
        </div>
       
        <div className="mb-3">
          <label htmlFor="categories" className="form-label">Categories</label>
          <select
            multiple
            className="form-control"
            id="categories"
            name="categories"
            value={formData.categoryIds.map(id => id.toString())}
            onChange={handleCategoryChange}
          >
            {categories.map(category => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </select>
          <div className="form-text">Hold Ctrl to select multiple categories</div>
        </div>
       
        <div className="mb-3">
          <label htmlFor="status" className="form-label">Status</label>
          <select
            className="form-control"
            id="status"
            name="status"
            value={formData.status}
            onChange={handleChange}
          >
            {renderStatusOptions()}
          </select>
        </div>
       
        <div className="mb-3">
          <label htmlFor="metaTitle" className="form-label">Meta Title</label>
          <input
            type="text"
            className="form-control"
            id="metaTitle"
            name="metaTitle"
            value={formData.metaTitle}
            onChange={handleChange}
          />
        </div>
       
        <div className="mb-3">
          <label htmlFor="metaDescription" className="form-label">Meta Description</label>
          <textarea
            className="form-control"
            id="metaDescription"
            name="metaDescription"
            rows="3"
            value={formData.metaDescription}
            onChange={handleChange}
          ></textarea>
        </div>
       
        <div className="mb-3">
          <label htmlFor="tags" className="form-label">Tags</label>
          <input
            type="text"
            className="form-control"
            id="tags"
            name="tags"
            value={formData.tags}
            onChange={handleChange}
            placeholder="Comma-separated tags"
          />
        </div>
       
        <button type="submit" className="btn btn-primary" disabled={loading}>
          {loading ? 'Saving...' : (isEditing ? 'Update Post' : 'Create Post')}
        </button>
        <button type="button" className="btn btn-secondary ms-2" onClick={() => navigate('/dashboard')}>
          Cancel
        </button>
      </form>
    </div>
  );
};

export default CreateEditPost;