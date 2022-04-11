import React, {useState} from 'react'
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';

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
        })
    }

    const fun = () => {
        document.getElementById('theDiv').innerHTML = 'Jucatorul ' + name + ' a fost adaugat.';
    }
    function myFunction(){
        postData();
        fun();
    }



    return (
    
    <Form className="create-form">
        <Form.Field>
            <label>name</label>
            <input placeholder='Name' onChange={(e) => setname(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label>nationality</label>
            <input placeholder='Nationality' onChange={(e) => setnationality(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label>birth_Date</label>
            <input placeholder='2021-12-12' onChange={(e) => setbirth_Date(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label>height</label>
            <input placeholder='Height' onChange={(e) => setheight(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label>foot</label>
            <input placeholder='Foot' onChange={(e) => setfoot(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label>position</label>
            <input placeholder='Position' onChange={(e) => setposition(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label>value</label>
            <input placeholder='Value' onChange={(e) => setvalue(e.target.value)}/>
        </Form.Field>        
        <Button onClick={myFunction} type = 'submit'>Submit</Button>
    </Form>
)
} 



