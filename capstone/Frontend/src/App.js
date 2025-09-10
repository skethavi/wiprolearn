import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './components/Header';
import Home from './pages/Home';
import Dashboard from './pages/Dashboard';
import Landing from './pages/Landing';
import Login from './pages/Login';
import Register from './pages/Register';
import CreateEditPost from './pages/CreateEditPost';
import Admin from './pages/Admin';
import PostDetail from './pages/PostDetail';
import { AuthProvider } from './context/AuthContext';

function App() {
  return (
    <AuthProvider>
      <Router>
        <div className="App">
          <Routes>
            <Route path="/" element={<Landing />} />
            <Route path="/home" element={<HomeWrapper />} />
            <Route path="/login" element={<LoginWrapper />} />
            <Route path="/register" element={<RegisterWrapper />} />
            <Route path="/dashboard" element={<DashboardWrapper />} />
            <Route path="/create-post" element={<CreateEditPostWrapper />} />
            <Route path="/edit-post/:id" element={<CreateEditPostWrapper />} />
            <Route path="/admin" element={<AdminWrapper />} />
            <Route path="/post/:id" element={<PostDetailWrapper />} />
          </Routes>
        </div>
      </Router>
    </AuthProvider>
  );
}

// Wrapper components to conditionally show Header
const HomeWrapper = () => {
  return (
    <>
      <Header />
      <Home />
    </>
  );
};

const LoginWrapper = () => {
  return <Login />;
};

const RegisterWrapper = () => {
  return <Register />;
};

const DashboardWrapper = () => {
  return (
    <>
      <Header />
      <Dashboard />
    </>
  );
};

const CreateEditPostWrapper = () => {
  return (
    <>
      <Header />
      <CreateEditPost />
    </>
  );
};

const AdminWrapper = () => {
  return (
    <>
      <Header />
      <Admin />
    </>
  );
};

const PostDetailWrapper = () => {
  return (
    <>
      <Header />
      <PostDetail />
    </>
  );
};

export default App;







