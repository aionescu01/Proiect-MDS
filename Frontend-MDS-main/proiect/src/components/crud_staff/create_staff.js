import React, {useState} from "react"
import {Button, Checkbox, Form} from "semantic-ui-react"
import axios from "axios";
//import './CS.css';

export default function Create_Staff(){
    const [name, setname] = useState('');
    const [role, setrole] = useState('');
    const [birth_date, setbirth_date] = useState(new Date('2021-12-16T10:43:46.737Z'));
    const [email, setemail] = useState('');
    const [phone_number, setphone_number] = useState('');


    const postData = () => {
        axios.post('https://localhost:44307/api/StaffMember/add-staff-member', {
            name,
            role,
            birth_date,
            email,
            phone_number
        },{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
    }

    const fun = () => { 
        document.getElementById('theDiv').innerHTML = 'Membrul din staff ' + name + ' a fost adaugat.';
    }
    function myFunction(){
        postData();
        window.location.href = 'staff';
    }

    return(
        <Form className="create-form1">
            <h2 className="bt2">Add Staff</h2>
            <Form.Field>
                <label>name</label>
                <input placeholder="Name" onChange={(e) => setname( e.target.value )} />
            </Form.Field>
            <Form.Field>
                <label>role</label>
                <input placeholder="Role" onChange={(e) => setrole( e.target.value )} />
            </Form.Field>
            <Form.Field>
                <label>birth_date</label>
                <input placeholder="Birth_Date" onChange={(e) => setbirth_date( e.target.value )}/>
            </Form.Field>
            <Form.Field>
                <label>email</label>
                <input placeholder="Email" onChange={(e) => setemail( e.target.value )}/>
            </Form.Field>
            <Form.Field>
                <label>phone_number</label>
                <input placeholder="Phone_Number" onChange={(e) => setphone_number( e.target.value )}/>
            </Form.Field>
            <Button className="bt2" onClick={myFunction} type = 'submit'>Submit</Button>
        </Form>
    )
}
