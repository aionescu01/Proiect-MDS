import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import "./index.css";
import * as serviceWorker from "./serviceWorker";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import {
    Navigation,
    Home,
    Players,
    Staff,
    Player_Contracts,
    Staff_Contracts
  } from "./components";
import Update_Players from './components/crud_players/update_players';

  ReactDOM.render(
    <Router>
      <Navigation />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/Players" element={<Players />} />
        <Route path="/Staff" element={<Staff />} />
        <Route path="/update_players" element={<Update_Players />} />
        <Route path="/Player_Contracts" element={<Player_Contracts />} />
        <Route path="/Staff_Contracts" element={<Staff_Contracts />} />
      </Routes>
    </Router>,
  
    document.getElementById("root")
  );


ReactDOM.render(<App />, document.getElementById('root'));


