import React, {useState} from "react";
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';


export default function Create_Match(){
    const [stadiumId, setstadiumid] = useState(0);
    const [opponent, setopponent] = useState('');
    const [competition, setcompetition] = useState('');
    const [event_date, setevent_date] = useState(new Date('2021-12-16T10:43:46.737Z'));
    const [score, setscore] = useState('');
    const [referee, setreferee] = useState('');

    const postData = () => {
        axios.post('https://localhost:44307/api/Match/add-match',{
            stadiumId,
            opponent,
            competition,
            event_date,
            score,
            referee
        },{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
    }
    const fun = () => {
        document.getElementById('theDiv').innerHTML = 'Meciul impotriva ' + opponent + ' a fost adaugat.';
    }
    function myFunction(){
        postData();

        window.location.href = 'http://localhost:3000/Matches'
    }

    return(
        <Form className="create-form1">
            <h2 className="bt2">Add a match</h2>
        <Form.Field>
            <label className='scris'>Stadium ID</label>
            <input className='raspuns' placeholder='Stadium ID' onChange={(e) => setstadiumid(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>Opponent</label>
            <input className='raspuns' placeholder='Opponent' onChange={(e) => setopponent(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>Competition</label>
            <input className='raspuns' placeholder='Competition' onChange={(e) => setcompetition(e.target.value)} />
        </Form.Field>
        <Form.Field>
            <label className='scris'>Event Date</label>
            <input className='raspuns' placeholder='2021-12-12' onChange={(e) => setevent_date(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label className='scris'>score</label>
            <input className='raspuns' placeholder='Score' onChange={(e) => setscore(e.target.value)}/>
        </Form.Field>
        <Form.Field>
            <label className='scris'>Referee</label>
            <input className='raspuns' placeholder='Referee' onChange={(e) => setreferee(e.target.value)}/>
        </Form.Field>
        
        <Button className='bt2' onClick={myFunction} type = 'submit'>Submit</Button>
    </Form>
    )
}

