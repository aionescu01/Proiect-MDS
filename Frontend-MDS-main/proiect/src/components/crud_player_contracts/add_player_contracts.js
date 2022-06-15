import React , { useEffect,useState }from 'react';
import { Form,Button, Input } from 'semantic-ui-react'
import axios from 'axios';
import { Link } from 'react-router-dom';
//import './RP.css';
import { body } from 'express-validator';

export default function Add_player_contracts() {

    const [Link, setlink] = useState('');
    const [Link2, setlink2] = useState('');

    const handleSubmit = (e) => {
        console.log(Link);
        console.log(Link2);
        console.log(Link+Link2);
    axios.post(`https://localhost:44307/api/Contract/add-players`,
    Link+Link2,
    { 
        headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}    
    }
    ).then(response=>{window.location.reload()})
} 
    return(

        
        <Form className="create-form1" onSubmit={handleSubmit}>
            <h2 className='bt2'>Link</h2>
            <Input className='inp' onChange={(e) => setlink(e.target.value)} value = {Link}></Input>
            <Input className='inp' onChange={(e) => setlink2(e.target.value)} value = {Link2}></Input>
        <Button className='bt2' type = 'submit'>Submit</Button>
    </Form>
    )

}
