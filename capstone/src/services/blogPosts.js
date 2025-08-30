import api from './api';export const blogPostService = {
  getAll: async () => {
    try {
      const response = await api.get('/blogposts');
      return response.data;
    } catch (error) {
      console.error('Error fetching posts:', error);
      throw error;
    }
  },
  getById: async (id) => {
    const response = await api.get(`/blogposts/${id}`);
    return response.data;
  },
  getUserPosts: async () => {
    const response = await api.get('/blogposts/user');
    return response.data;
  },
  create: async (postData) => {
    const response = await api.post('/blogposts', postData);
    return response.data;
  },
  update: async (id, postData) => {
    const response = await api.put(`/blogposts/${id}`, postData);
    return response.data;
  },
  delete: async (id) => {
    const response = await api.delete(`/blogposts/${id}`);
    return response.data;
  },
  getPending: async () => {
    const response = await api.get('/blogposts/admin/pending');
    return response.data;
  },
  approve: async (id) => {
    const response = await api.put(`/blogposts/admin/approve/${id}`);
    return response.data;
  },
  reject: async (id) => {
    const response = await api.put(`/blogposts/admin/reject/${id}`);
    return response.data;
  }
};