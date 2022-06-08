import React, { useState,useEffect } from 'react';
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';
import { useNavigate } from "react-router-dom";


export default function Update_Staff() {
    useEffect(() => {
        setid(localStorage.getItem('ID'))
        setname(localStorage.getItem('Name'));
        setrole(localStorage.getItem('Role'));
        setbirth_date(localStorage.getItem('Birth Date'))
        setemail(localStorage.getItem('Email'));
        setphone_number(localStorage.getItem('Phone Number'));
        
}, []);

    const [id, setid] = useState(null);
    const [name, setname] = useState('');
    const [role, setrole] = useState('');
    const [birth_date, setbirth_date] = useState(new Date('2021-12-16T10:43:46.737Z'));
    const [email, setemail] = useState('');
    const [phone_number, setphone_number] = useState('');

    const updateAPIData = () => {
        let navigate = useNavigate(); 
        let path = `staff`; 
        navigate(path);
        axios.put(`https://localhost:44307/api/StaffMember/put-by-id/${id}`, {
            name,
            role,
            birth_date,
            email,
            phone_number
        })
    }
    

    useEffect(() => {
            setid(localStorage.getItem('ID'))
            setname(localStorage.getItem('Name'));
            setrole(localStorage.getItem('Role'));
            setbirth_date(localStorage.getItem('Birth Date'))
            setemail(localStorage.getItem('Email'));
            setphone_number(localStorage.getItem('Phone Number'));
            
    }, []);

    return (
        
        <form> 
        <div>
           <Form.Field>
            <label>name</label>
            <input placeholder={name} defaultValue={name} onChange={(e) => setname(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label>role</label>
            <input placeholder={role} defaultValue={role} onChange={(e) => setrole(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label>birth_date</label>
            <input placeholder={birth_date} defaultValue={birth_date} onChange={(e) => setbirth_date(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label>email</label>
            <input placeholder={email} defaultValue={email} onChange={(e) => setemail(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label>phone_number</label>
            <input placeholder={phone_number} defaultValue={phone_number}  onChange={(e) => setphone_number(e.target.value)}/>
        </Form.Field>
        </div>
        <Button type='submit' onClick={updateAPIData}>Update</Button>
        </form>
    )
}
