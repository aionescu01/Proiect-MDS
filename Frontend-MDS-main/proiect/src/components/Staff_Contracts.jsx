import React from "react";
import Create_Staff_Contracts from "./crud_staff_contracts/create_staff_contracts";
import Read_Staff_Contracts from "./crud_staff_contracts/read_staff_contracts";
import Update_Staff_Contracts from "./crud_staff_contracts/update_staff_contracts";
import Add_staff_contracts from "./crud_staff_contracts/add_staff_contracts";
import { BrowserRouter as Router, Route } from 'react-router-dom';

import './Players.css';

function Staff_Contracts() {
  return (
   
    <div className="main">

        
<div className="pls1">
<div className="pls2">
         <Add_staff_contracts/>    

            </div>
            <div className="pls2">
            
<Create_Staff_Contracts/>
      </div>
         
      </div>
        <h2 className="main-header">Staff Contracts</h2>

       
        <div className="pls">
           <Read_Staff_Contracts/> 
        
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
  
  export default Staff_Contracts;
