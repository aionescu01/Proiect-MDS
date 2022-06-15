import React, { useState,useEffect } from 'react';
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';
import { useNavigate } from "react-router-dom";


export default function Update_Match_Players() {
    useEffect(() => {
        setplayerid(localStorage.getItem('Player ID'))
        setmatchid(localStorage.getItem('Match ID'));
        
}, []);

    const [playerid, setplayerid] = useState(null);
    const [matchid, setmatchid] = useState(null);
    
    const updateAPIData = () => {
        // console.log(id,name,nationality,height.toString().substring(0, (height.toString()).length-2))
        let navigate = useNavigate(); 
        let path = `/Match_Players`; 
        navigate(path);
        //put by id
        axios.put(`https://localhost:44307/api/MatchPlayer/get-by-PlayerId/${playerid}`, {
            matchid
        })
    }
    

    useEffect(() => {
            setplayerid(localStorage.getItem('Player ID'));
            setmatchid(localStorage.getItem('Match ID'));
            
    }, []);


        

    return (
        
        <form> 
        <div>
        
           <Form.Field>
            <label>Player ID</label>
            <input placeholder={playerid} defaultValue={playerid} onChange={(e) => setplayerid(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label>Match ID</label>
            <input placeholder={matchid} defaultValue={matchid} onChange={(e) => setmatchid(e.target.value)} />
        </Form.Field>
        
        </div>
        <Button type='submit' onClick={updateAPIData}>Update</Button>
        </form>
    )
}
