import React, {useState} from "react";
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';


export default function Create_Match_Staff(){
    const [matchId, setmatchid] = useState(0);
    const [staffmemberid, setstaffmemberid] = useState(0);
    
    const postData = () => {
        axios.post('https://localhost:44307/api/MatchStaff/add-matchstaff',{
            matchId,
            staffmemberid
        })
    }
    const fun = () => {
        document.getElementById('theDiv').innerHTML = 'Membrul staffului a fost adaugat.';
    }
    function myFunction(){
        postData();

        window.location.href = 'http://localhost:3000/Match_Staff'
    }

    return(
        <Form className="create-form">
            <Form.Field>
            <label className='scris'>Match ID</label>
            <input className='raspuns' placeholder='Match ID' onChange={(e) => setmatchid(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>Staff ID</label>
            <input className='raspuns' placeholder='Staff ID' onChange={(e) => setstaffid(e.target.value)} />
        </Form.Field>
        
        
        <Button onClick={myFunction} type = 'submit'>Submit</Button>
    </Form>
    )
}

