import React from "react";
import Create_Players from './crud_players/create_players';
import Add_players from "./crud_players/add_players";
import Read_Players from './crud_players/read_players';
import Update_Players from './crud_players/update_players';
import './Players.css';
import { BrowserRouter as Router, Route } from 'react-router-dom'
function Players() {
    return (
      <div className="main">

        
<div className="pls1">
<div className="pls2">
         <Add_players/>    

            </div>
            <div className="pls2">
            
<Create_Players/>
      </div>
         
      </div>
        <h2 className="main-header">Players List</h2>

       
        <div className="pls">
           <Read_Players/> 
        
      </div>
      {/* <h2 className="main-header">Add a player</h2>  */}
      <h3><div id="theDiv"></div></h3>
      <div className="pls">
            
        
      </div>
      <div className="pls">
            

      </div>
      </div>
      
      
    );
  }
  
  export default Players;
