import React, {useState} from "react";
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';


export default function Create_Match_Players(){
    const [matchId, setmatchid] = useState(0);
    const [playerid, setplayerid] = useState(0);
    
    const postData = (e) => {
        e.preventDefault();
        axios.post('https://localhost:44307/api/MatchPlayer/add-matchplayer',{
            matchId,
            playerid
        },{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
        .then(window.location.reload());
    }
    const fun = () => {
        document.getElementById('theDiv').innerHTML = 'Jucatorul a fost adaugat.';
    }


    return(
        <Form className="create-form1">
             <h2 className='bt2'>Add match player</h2>
            <Form.Field>
            <label className='scris'>Match ID</label>
            <input className='raspuns' placeholder='Match ID' onChange={(e) => setmatchid(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>Player ID</label>
            <input className='raspuns' placeholder='Player ID' onChange={(e) => setplayerid(e.target.value)} />
        </Form.Field>
        
        
        <Button className="bt2" onClick={e=>(postData(e))} type = 'submit'>Submit</Button>
    </Form>
    )
}

