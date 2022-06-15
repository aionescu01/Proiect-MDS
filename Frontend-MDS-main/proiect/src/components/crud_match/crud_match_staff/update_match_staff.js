import React, { useState,useEffect } from 'react';
import { Button, Checkbox, Form } from 'semantic-ui-react'
import axios from 'axios';
import { useNavigate } from "react-router-dom";


export default function Update_Match_Staff() {
    useEffect(() => {
        setstaffid(localStorage.getItem('Staff ID'))
        setmatchid(localStorage.getItem('Match ID'));
        
}, []);

    const [staffmemberid, setstaffmemberid] = useState(null);
    const [matchid, setmatchid] = useState(null);
    
    const updateAPIData = () => {
        // console.log(id,name,nationality,height.toString().substring(0, (height.toString()).length-2))
        let navigate = useNavigate(); 
        let path = `/Match_Staff`; 
        navigate(path);
        //put by id
        axios.put(`https://localhost:44307/api/MatchStaff/get-by-StaffMemberId${staffmemberid}`, {
            matchid
        })
    }
    

    useEffect(() => {
            setstaffmemberid(localStorage.getItem('Staff ID'))
            setmatchid(localStorage.getItem('Match ID'));
            
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
            <label>Staff ID</label>
            <input placeholder={staffmemberid} defaultValue={staffmemberid} onChange={(e) => setstaffmemberid(e.target.value)} />
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
