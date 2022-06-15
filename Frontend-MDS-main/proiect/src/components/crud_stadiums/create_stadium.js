import React, {useState} from 'react'
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';
//import './CS.css';

export default function Create_Stadium() {
    const [name, setname] = useState('');
    const [capacity, setcapacity] = useState(0);
    const [surface, setsurface] = useState('');
    const [address, setaddress] = useState('');
    
    const postData = () => {
        
        axios.post(`https://localhost:44307/api/Stadium/add-stadium`, {
            name,
            capacity,
            surface,
            address
        },{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
    }

    const fun = () => {
        document.getElementById('theDiv').innerHTML = 'Stadionul ' + name + ' a fost adaugat.';
    }
    function myFunction(){
        postData();
        window.location.href = 'http://localhost:3000/Stadiums'
    }



    return (
    
        <Form className="create-form1">
        <h2 className="bt2">Add a stadium</h2>
        <Form.Field>
            <label className='scris'>name</label>
            <input className='raspuns' placeholder='Name' onChange={(e) => setname(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>capacity</label>
            <input className='raspuns' placeholder='Capacity' onChange={(e) => setcapacity(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>surface</label>
            <input className='raspuns' placeholder='Surface' onChange={(e) => setsurface(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>address</label>
            <input className='raspuns' placeholder='Address' onChange={(e) => setaddress(e.target.value)}/>
        </Form.Field>
        <Button className='bt2' onClick={myFunction} type = 'submit'>Submit</Button>
    </Form>
)
} 



