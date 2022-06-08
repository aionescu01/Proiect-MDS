import React, { useState,useEffect } from 'react';
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';
import { useNavigate } from "react-router-dom";


export default function Update_Players() {
    useEffect(() => {
        setid(localStorage.getItem('ID'))
        setname(localStorage.getItem('Name'));
        setnationality(localStorage.getItem('Nationality'));
        setbirth_Date(localStorage.getItem('Birth Date'))
        setheight(localStorage.getItem('Height'));
        setfoot(localStorage.getItem('Foot'));
        setposition(localStorage.getItem('Position'));
        setvalue(localStorage.getItem('Value'));
        
}, []);

    const [id, setid] = useState(null);
    const [name, setname] = useState('');
    const [nationality, setnationality] = useState('');
    const [birth_Date, setbirth_Date] = useState(new Date('2021-12-16T10:43:46.737Z'));
    const [height, setheight] = useState(0);
    const [foot, setfoot] = useState('');
    const [position, setposition] = useState('');
    const [value, setvalue] = useState(0);

    const updateAPIData = () => {
        // console.log(id,name,nationality,height.toString().substring(0, (height.toString()).length-2))
        let navigate = useNavigate(); 
        let path = `/Players`; 
        navigate(path);
        axios.put(`https://localhost:44307/api/Player/put-by-id/${id}`, {
            name,
            nationality,
            birth_Date,
            height,
            foot,
            position,
            value
        })
    }
    

    useEffect(() => {
            setid(localStorage.getItem('ID'))
            setname(localStorage.getItem('Name'));
            setnationality(localStorage.getItem('Nationality'));
            setbirth_Date(localStorage.getItem('Birth Date'))
            setheight(localStorage.getItem('Height'));
            setfoot(localStorage.getItem('Foot'));
            setposition(localStorage.getItem('Position'));
            setvalue(localStorage.getItem('Value'));
            
    }, []);

    // axios.interceptors.request.use(request => {
    //     console.log('Starting Request', JSON.stringify(request, null, 2))
    //     return request
    //   })

        

    return (
        
        <form> 
        <div>
        {/* <Form.Field>
            <label>id</label>
            <input placeholder='ID' value={id || 0} onChange={(e) => setid(e.target.id)} />
        </Form.Field> */}
           <Form.Field>
            <label>name</label>
            <input placeholder={name} defaultValue={name} onChange={(e) => setname(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label>nationality</label>
            <input placeholder={nationality} defaultValue={nationality} onChange={(e) => setnationality(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label>birth_Date</label>
            <input placeholder={birth_Date} defaultValue={birth_Date} onChange={(e) => setbirth_Date(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label>height</label>
            <input placeholder={height} defaultValue={localStorage.getItem('Height')} onChange={(e) => setheight(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label>foot</label>
            <input placeholder={foot} defaultValue={foot} onChange={(e) => setfoot(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label>position</label>
            <input placeholder={position} defaultValue={position}  onChange={(e) => setposition(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label>value</label>
            <input placeholder={value} defaultValue={localStorage.getItem('Value')} onChange={(e) => setvalue(e.target.value)}/>
        </Form.Field>
        </div>
        <Button type='submit' onClick={updateAPIData}>Update</Button>
        </form>
    )
}
