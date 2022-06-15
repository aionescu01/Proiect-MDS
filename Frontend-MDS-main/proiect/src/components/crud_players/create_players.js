import React, {useState} from 'react'
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';
import './CP.css';

export default function Create_Players() {
    const [name, setname] = useState('');
    const [nationality, setnationality] = useState('');
    const [birth_Date, setbirth_Date] = useState(new Date('2021-12-16T10:43:46.737Z'));
    const [height, setheight] = useState(0);
    const [foot, setfoot] = useState('');
    const [position, setposition] = useState('');
    const [value, setvalue] = useState(0);
    
    const postData = () => {
        
        axios.post(`https://localhost:44307/api/Player/add-one-player`, {
            name,
            nationality,
            birth_Date,
            height,
            foot,
            position,
            value
            
        },{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
    }

    const fun = () => {
        document.getElementById('theDiv').innerHTML = 'Jucatorul ' + name + ' a fost adaugat.';
    }
    function myFunction(){
        postData();

       window.location.href = 'http://localhost:3000/Players'
    }



    return (
    
    <Form className="create-form1">
        <h2 className="bt2">Add a player</h2>
        <Form.Field>
            <label className='scris'>name</label>
            <input className='raspuns' placeholder='Name' onChange={(e) => setname(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>nationality</label>
            <input className='raspuns' placeholder='Nationality' onChange={(e) => setnationality(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>birth_Date</label>
            <input className='raspuns' placeholder='2021-12-12' onChange={(e) => setbirth_Date(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>height</label>
            <input className='raspuns' placeholder='Height' onChange={(e) => setheight(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label className='scris'>foot</label>
            <input className='raspuns' placeholder='Foot' onChange={(e) => setfoot(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label className='scris'>position</label>
            <input className='raspuns' placeholder='Position' onChange={(e) => setposition(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label className='scris'>value</label>
            <input className='raspuns' placeholder='Value' onChange={(e) => setvalue(e.target.value)}/>
        </Form.Field>        
        <Button className='bt2' onClick={myFunction} type = 'submit'>Submit</Button>
    </Form>
)
} 



