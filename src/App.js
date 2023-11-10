import React from 'react';
import {  BrowserRouter as Router, Routes, Route} from 'react-router-dom';
import Loginpage from './Components/Loginpage';
import Dashboard from './Components/Dashboard';
import Lastpage from './Components/Lastpage';

function App() {
  return (
    <Router>
    <Routes>
      <Route path="/" element={<Loginpage />} />
      <Route path="/dashboard" element={<Dashboard />} />
      <Route path="/lastpage" element={<Lastpage />} />
    </Routes>
  </Router>
  );
}

export default App;
