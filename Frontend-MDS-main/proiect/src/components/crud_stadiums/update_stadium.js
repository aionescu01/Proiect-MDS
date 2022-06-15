import React, { useState,useEffect } from 'react';
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';
import { useNavigate } from "react-router-dom";


export default function Update_Stadium() {
    useEffect(() => {
        setstadiumid(localStorage.getItem('Stadium ID'))
        setname(localStorage.getItem('Name'));
        setcapacity(localStorage.getItem('Capacity'));
        setsurface(localStorage.getItem('Surface'))
        setaddress(localStorage.getItem('Address'));
        setid(localStorage.getItem('Stadium ID'));
}, []);

    const [stadiumId, setstadiumid] = useState();
    const [name, setname] = useState();
    const [capacity, setcapacity] = useState();
    const [surface, setsurface] = useState();
    const [address, setaddress] = useState();
    const [id, setid] = useState();
    
    const updateAPIData = (e) => {
        e.preventDefault();
        axios.put(`https://localhost:44307/api/Stadium/put-by-id/${id}`, {
            name,
            capacity,
            surface,
            address
        },{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
        .then(response =>(window.location.assign("/Stadiums")));
    }
    

    useEffect(() => {
        setstadiumid(localStorage.getItem('Stadium ID'))
        setname(localStorage.getItem('Name'));
        setcapacity(localStorage.getItem('Capacity'));
        setsurface(localStorage.getItem('Surface'))
        setaddress(localStorage.getItem('Address'));
            
    }, []);

   
    return (
        
        <form className='create-form1'> 
        <div>
        
           <Form.Field className='ff'>
            <label>name</label>
            <input placeholder={name} defaultValue={name} onChange={(e) => setname(e.target.value)} />
        </Form.Field>
        <Form.Field className='ff'>
            <label>capacity</label>
            <input placeholder={capacity} defaultValue={capacity} onChange={(e) => setcapacity(e.target.value)} />
        </Form.Field>
        <Form.Field className='ff'>
            <label>surface</label>
            <input placeholder={surface} defaultValue={surface} onChange={(e) => setsurface(e.target.value)} />
        </Form.Field>
        <Form.Field className='ff'>
            <label>address</label>
            <input placeholder={address} defaultValue={address} onChange={(e) => setaddress(e.target.value)}/>
        </Form.Field>
        
        </div>
        <Button className='b1' type='submit' onClick={(e=>(updateAPIData(e)))}>Update</Button>
        </form>
    )
}
