import React from "react";
import Create_Match from './crud_match/create_match';
import Read_Match from "./crud_match/read_match";
import Update_Match from './crud_match/update_match';
import Create_Match_Players from "./crud_match/crud_match_players/create_match_players";
import './Players.css';
import { BrowserRouter as Router, Route } from 'react-router-dom'
function Matches() {
    return (
      <div className="main">
     <div className="pls1">
<div className="pls2">
         <Create_Match/>    

            </div>
            <div className="pls2">
            
<Create_Match_Players/>
      </div>
         
      </div>
      <h2 className="main-header">Matches List</h2>

   
    <div className="pls">
       <Read_Match/> 
    
  </div>
 
  <h3><div id="theDiv"></div></h3>
  <div className="pls">
        
    
  </div>
  <div className="pls">
        

  </div>
    </div>
      
      
      
    );
  }
  
  export default Matches;
