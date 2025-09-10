import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import EmployeeShow from './components/employeeShow/employeeShow';
import DashBoard from './components/dashboard/dashboard';
import ApplyLeave from './components/applyLeave/applyLeave';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
         <Route path="/" element={<EmployeeShow />} /> 
          <Route path="/dashBoard" element={<DashBoard />} />
          <Route path="/applyLeave" element={<ApplyLeave />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
