import React , { useEffect,useState }from 'react';
import { Form,Button } from 'semantic-ui-react'
import axios from 'axios';
import { Link } from 'react-router-dom';
import './RP.css';

export default function Add_players() {

const postData = () => {
    const [Link, setlink] = useState('');
    //?    
    axios.post(`https://localhost:44307/api/Staff/Add one staff member${Link}`, {
        Link
        
    })
}
    return(
        <Form className="create-form">
        <Form.Field>
            <label className='Link'>Link</label>
            <input className='raspuns' placeholder='Link' />
        </Form.Field>    
        <Button onClick={postData(Link)} type = 'submit'>Submit</Button>
    </Form>
    )

    }
