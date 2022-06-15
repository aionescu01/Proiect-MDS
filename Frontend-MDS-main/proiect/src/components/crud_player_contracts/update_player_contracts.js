import React, { useState,useEffect } from 'react';
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';
import { useNavigate } from "react-router-dom";


export default function Update_Player_Contracts() {


    const [start_date, setstart_date] = useState();
    const [end_date, setend_date] = useState();
    const [salary, setsalary] = useState();
    const [agent, setagent] = useState('');
    const [playerId, setplayerid] = useState(0);
    const [contractid, setcontractid] = useState(0);
    const [staffMemberId, setstaffMemberId] = useState(0);

    useEffect(() => {
        
        setstart_date(localStorage.getItem('Start Date'));
        setend_date(localStorage.getItem('End Date'));
        setsalary(localStorage.getItem('Salary'))
        setagent(localStorage.getItem('Agent'));
        setplayerid(localStorage.getItem('Player ID'));
        setcontractid(localStorage.getItem('Contract ID'));
}, []);

 
    function updateAPIData (e) {
        console.log(contractid,start_date,end_date,salary,agent,playerId,staffMemberId);
        
        e.preventDefault();
        axios.put(`https://localhost:44307/api/Contract/put-by-id/${contractid}`, {
            start_date,
            end_date,
            salary,
            agent,
            playerId,
            staffMemberId
           
        },{headers: {'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt")}`}})
        .then(response =>(window.location.assign("/Player_Contracts")));
    }

    
        

    return (
        
        <form className="create-form"> 
        <div>
           <Form.Field className='ff'>
            <label>Start Date</label>
            <input placeholder={start_date} defaultValue={start_date} onChange={(e) => setstart_date(e.target.value)} />
        </Form.Field>
        <Form.Field className='ff'>
            <label>End Date</label>
            <input placeholder={end_date} defaultValue={end_date} onChange={(e) => setend_date(e.target.value)} />
        </Form.Field>
        <Form.Field className='ff'>
            <label>Salary</label>
            <input placeholder={salary} defaultValue={salary} onChange={(e) => setsalary(e.target.value)} />
        </Form.Field>
        <Form.Field className='ff'>
            <label>Agent</label>
            <input placeholder={agent} defaultValue={agent} onChange={(e) => setagent(e.target.value)} />
        </Form.Field>
        
        </div>
        <Button className='bt2' type='submit' onClick={e=>(updateAPIData(e))}>Update</Button>
        </form>
    )
}
