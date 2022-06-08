import React from "react";
import Create_Player_Contracts from './crud_player_contracts/create_player_contracts';
import Read_Player_Contracts from "./crud_player_contracts/read_player_contracts";
import Update_Player_Contracts from "./crud_player_contracts/update_player_contracts";
import './Players.css';
import { BrowserRouter as Router, Route } from 'react-router-dom'
function Staff() {
  return (
    <div className="main">

      <h2 className="main-header">Player Contracts List</h2>
      <div className="pls">
         <Read_Player_Contracts/> 
      
    </div>
    <h2 className="main-header">Add a Player Contract</h2> 
    <h3><div id="theDiv"></div></h3>
    <div className="pls">
          
      <Create_Player_Contracts/>
    </div>

    <h2 className="main-header">Update a Player Contract</h2> 
    <h3><div id="theDiv"></div></h3>
    <div className="pls">
          
      <Update_Player_Contracts/>
    </div>
    </div>
    
    
  );
  }
  
  export default Player_Contracts;
