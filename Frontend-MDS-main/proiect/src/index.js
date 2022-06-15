import React from 'react';
import ReactDOM from 'react-dom';
import "./index.css";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import {
    Navigation,
    Home,
    Players,
    Staff,
    Player_Contracts,
    Staff_Contracts,
    Matches,
    Stadiums,
    Register_component,
    Login_component,
    Profile_component
    

  } from "./components";

import Update_Staff_Contracts from './components/crud_staff_contracts/update_staff_contracts';
import Stats from './components/Stats';
import Update_Match from './components/crud_match/update_match';
import Update_Stadium from './components/crud_stadiums/update_stadium';
import Read_Match_Players from './components/crud_match/crud_match_players/read_match_players';
import Update_Players from './components/crud_players/update_players';
import Update_Staff from './components/crud_staff/update_staff';
import Update_Player_Contracts from './components/crud_player_contracts/update_player_contracts';



  ReactDOM.render(
    <Router>
      <Navigation />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/Players" element={<Players />} />
        <Route path="/Staff" element={<Staff />} />
        <Route path="/update_players" element={<Update_Players />} />
        <Route path="/update_player_contract" element={<Update_Player_Contracts />} />
        <Route path="/update_staff" element={<Update_Staff />} />
        <Route path="/Player_Contracts" element={<Player_Contracts />} />
        <Route path="/Staff_Contracts" element={<Staff_Contracts />} />
        <Route path="/Matches" element={<Matches />} />
        <Route path="/Stadiums" element={<Stadiums />} />
        <Route path="/register" element={<Register_component />} />
        <Route path="/login" element={<Login_component />} />
        <Route path="/profile" element={<Profile_component />} />
        <Route path="/mplayers" element={<Read_Match_Players />} />
        <Route path="/update_match" element={<Update_Match />} />
        <Route path="/update_stadium" element={<Update_Stadium />} />
        <Route path="/stats" element={<Stats />} />
        <Route path="/update_staff_contracts" element={<Update_Staff_Contracts />} />
        {/* <Route path="/stats" element={<Stats1 />} /> */}
      </Routes>
    </Router>,
  
    document.getElementById("root")
  );




