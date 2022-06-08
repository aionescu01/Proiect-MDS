import React from "react";
import Create_Staff_Contracts from "./crud_staff_contracts/create_staff_contracts";
import Read_Staff_contracts from "./crud_staff_contracts/read_staff_contracts";
import Update_Staff_Contracts from "./crud_staff_contracts/update_staff_contracts";


import './Players.css';
import { BrowserRouter as Router, Route } from 'react-router-dom'
function Staff() {
  return (
    <div className="main">

      <h2 className="main-header">Staff Contracts List</h2>
      <div className="pls">
         <Read_Staff_Contracts/> 
      
    </div>
    <h2 className="main-header">Add a Staff Contract</h2> 
    <h3><div id="theDiv"></div></h3>
    <div className="pls">
          
      <Create_Staff_Contracts/>
    </div>

    <h2 className="main-header">Update a Staff Contract</h2> 
    <h3><div id="theDiv"></div></h3>
    <div className="pls">
          
      <Update_Staff_Contracts/>
    </div>
    </div>
    
    
  );
  }
  
  export default Staff_Contracts;
