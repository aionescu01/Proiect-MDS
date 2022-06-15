import React, { useState } from "react";
import { Button, Checkbox, Form } from "semantic-ui-react"
import axios from "axios";



export default function Create_Staff_Contracts() {
    const [start_date, setstart_date] = useState(new Date('2021-12-16T10:43:46.737Z'));
    const [end_date, setend_date] = useState(new Date('2021-12-16T10:43:46.737Z'));
    const [salary, setsalary] = useState(0);
    const [agent, setagent] = useState('');
    const [staffmemberid, setstaffmemberid] = useState(0);

    const postData = () => {
        axios.post('https://localhost:44307/api/Contract/add-one-contract', {
            start_date,
            end_date,
            salary,
            agent,
            staffmemberid
        },{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
    }

    const fun = () => {
        document.getElementById('theDiv').innerHTML = 'Contractul persoanei din staff a fost adaugat.';
    }
    function myFunction(){
        postData();
        //fun();
        window.location.href = 'http://localhost:3000/Staff_Contracts';
    }

    return(
        <Form className="create-form1">
            <h2 className="bt2">Add a Staff Contract</h2>
        <Form.Field>
            <label className="scris">start_date</label>
            <input className="raspuns" placeholder='2021-12-12' onChange={(e) => setstart_date(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>end_date</label>
            <input className='raspuns'placeholder='2021-12-12' onChange={(e) => setend_date(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>salary</label>
            <input  className='raspuns'placeholder='Salary' onChange={(e) => setsalary(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>agent</label>
            <input className='raspuns'placeholder='Agent' onChange={(e) => setagent(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label className='scris'>staff id</label>
            <input className='raspuns'placeholder='StaffMemberId' onChange={(e) => setstaffmemberid(e.target.value)}/>
        </Form.Field>
        <Button className='bt2'onClick={myFunction} type = 'submit'>Submit</Button>
    </Form> 
    )
}
