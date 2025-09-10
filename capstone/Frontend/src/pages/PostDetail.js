import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { blogPostService } from '../services/blogPosts';

const PostDetail = () => {
  const [post, setPost] = useState(null);
  const [loading, setLoading] = useState(true);
  const { id } = useParams();

  useEffect(() => {
    const fetchPost = async () => {
      try {
        const data = await blogPostService.getById(id);
        setPost(data);
      } catch (error) {
        console.error('Error fetching post:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchPost();
  }, [id]);

  if (loading) return <div className="container mt-4">Loading...</div>;
  if (!post) return <div className="container mt-4">Post not found</div>;

  return (
    <div className="container mt-4">
      <article>
        <h1>{post.title}</h1>
        {post.featuredImageUrl && (
          <img 
            src={post.featuredImageUrl} 
            alt={post.title} 
            className="img-fluid mb-4"
            style={{ maxHeight: '400px', objectFit: 'cover', width: '100%' }}
          />
        )}
        <div className="text-muted mb-3">
          Posted by {post.author?.username} on {new Date(post.publishedAt).toLocaleDateString()}
        </div>
        <div dangerouslySetInnerHTML={{ __html: post.content }} />
        {post.tags && (
          <div className="mt-4">
            <strong>Tags:</strong> {post.tags}
          </div>
        )}
      </article>
    </div>
  );
};

export default PostDetail;