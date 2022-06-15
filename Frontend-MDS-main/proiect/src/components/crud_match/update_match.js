import React, { useState,useEffect } from 'react';
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';
import { useNavigate } from "react-router-dom";

export default function Update_Match() {
    useEffect(() => {
        setstadiumId(localStorage.getItem('Stadium ID'))
        setopponent(localStorage.getItem('Opponent'));
        setcompetition(localStorage.getItem('Competition'));
        setevent_date(localStorage.getItem('Event Date'))
        setscore(localStorage.getItem('Score'));
        setreferee(localStorage.getItem('Referee'));
        setid(localStorage.getItem('Match ID'));
        
}, []);

    const [stadiumId, setstadiumId] = useState(null);
    const [opponent, setopponent] = useState('');
    const [competition, setcompetition] = useState('');
    const [event_date, setevent_date] = useState();
    const [score, setscore] = useState();
    const [referee, setreferee] = useState('');
    const [id,setid] = useState(0);
    
    const updateAPIData = (e) => {
        
        e.preventDefault();
        axios.put(`https://localhost:44307/api/Match/put-by-id/${id}`, {

            stadiumId,
            opponent,
            competition,
            event_date,
            score,
            referee
        },{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
        .then(response =>(window.location.assign("/Matches")));
    }



    useEffect(() => {
        setstadiumId(localStorage.getItem('Stadium ID'));
        setopponent(localStorage.getItem('Opponent'));
        setcompetition(localStorage.getItem('Competition'));
        setevent_date(localStorage.getItem('Event Date'))
        setscore(localStorage.getItem('Score'));
        setreferee(localStorage.getItem('Referee'));
            
    }, []);


        

    return (
        
        <form className='create-form'> 
        <div>
        
           <Form.Field className='ff'>
            <label>Stadium ID</label>
            <input placeholder={stadiumId} defaultValue={stadiumId} onChange={(e) => setstadiumId(e.target.value)} />
        </Form.Field>
        <Form.Field className='ff'>
            <label>Opponent</label>
            <input placeholder={opponent} defaultValue={opponent} onChange={(e) => setopponent(e.target.value)} />
        </Form.Field>
        <Form.Field className='ff'>
            <label>Competition</label>
            <input placeholder={competition} defaultValue={competition} onChange={(e) => setcompetition(e.target.value)} />
        </Form.Field>
        <Form.Field className='ff'>
            <label>Event Date</label>
            <input placeholder={event_date} defaultValue={event_date} onChange={(e) => setevent_date(e.target.value)}/>
        </Form.Field>
        <Form.Field className='ff'>
            <label>Score</label>
            <input placeholder={score} defaultValue={score} onChange={(e) => setscore(e.target.value)}/>
        </Form.Field>
        <Form.Field className='ff'>
            <label>Referee</label>
            <input placeholder={referee} defaultValue={referee}  onChange={(e) => setreferee(e.target.value)}/>
        </Form.Field>
        </div>
        <Button className='b1' type='submit' onClick={e=>(updateAPIData(e))}>Update</Button>
        </form>
    )
}
