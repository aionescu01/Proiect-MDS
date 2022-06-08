import React from "react";
import Create_Staff from './crud_staff/create_staff';
import Add_Staff from './crud_staff/add_staff';
import Read_Staff from './crud_staff/read_staff';
import Update_Staff from './crud_staff/update_staff';
import './Players.css';
import { BrowserRouter as Router, Route } from 'react-router-dom'
function Staff() {
  return (
    <div className="main">

      
    <div className="pls">
         <Add_Staff/> 
      
    </div>
      <h2 className="main-header">Staff List</h2>
      <div className="pls">
         <Read_Staff/> 
      
    </div>
    <h2 className="main-header">Add a Staff Member</h2> 
    <h3><div id="theDiv"></div></h3>
    <div className="pls">
          
      <Create_Staff/>
    </div>

    <h2 className="main-header">Update a Staff Member</h2> 
    <h3><div id="theDiv"></div></h3>
    <div className="pls">
          
      <Update_Staff/>
    </div>
    </div>
    
    
  );
  }
  
  export default Staff;
