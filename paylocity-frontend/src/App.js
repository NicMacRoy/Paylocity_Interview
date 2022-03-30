
import './App.css';
import Home from './components/Home';
import Employees from './components/Employees'
import NavMenu2 from './components/NavMenu2';
import Employee from './components/Employee';
import CompanyReport from './components/CompanyReport';
import DependantAlter from './components/DependantAlter';
import EmployeeAdd from './components/EmployeeAdd';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
      <div className="App Container">
        <NavMenu2 />

       

        <Routes>
          <Route path='/home' element={<Home />} />
          <Route path='/employees' element={<Employees />} />
          <Route path='/employee' element={<Employee />} />
          <Route path='/report' element={<CompanyReport />} />
          <Route path='/dependant' element={<DependantAlter />} />
          <Route path='/addemployee' element={<EmployeeAdd />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
