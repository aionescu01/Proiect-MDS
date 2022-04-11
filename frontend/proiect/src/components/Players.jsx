import React from "react";
import Create_Players from './crud_players/create_players';
import Read_Players from './crud_players/read_players';
import './Players.css';
import { BrowserRouter as Router, Route } from 'react-router-dom'
function Players() {
    return (
      <div className="main">
        <h2 className="main-header">Players List</h2>
        <h3><div id="theDiv"></div></h3>
        <div className="pls">
            
        <Create_Players/>
      </div>
      <div className="pls">
            
        <Read_Players/>
      </div>
      </div>
      
      
    );
  }
  
  export default Players;
  