import React from "react";
import Create_Staff from './crud_staff/create_staff';
import Read_Staff from './crud_staff/read_staff';
import Update_Staff from './crud_staff/update_staff';
import Add_staff from "./crud_staff/add_staff";
import './Players.css';
import { BrowserRouter as Router, Route } from 'react-router-dom'
function Staff() {
  return (
    <div className="main">

        
<div className="pls1">
<div className="pls2">
         <Add_staff/>    

            </div>
            <div className="pls2">
            
<Create_Staff/>
      </div>
         
      </div>
        <h2 className="main-header">Staff List</h2>

       
        <div className="pls">
           <Read_Staff/> 
        
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
  
  export default Staff;
