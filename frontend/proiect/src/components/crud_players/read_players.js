import React , { useEffect,useState }from 'react';
import { Table } from 'semantic-ui-react'
import axios from 'axios';


export default function Read_Players() {
    const [APIData, setAPIData] = useState([]);
useEffect(() => {
    axios.get(`https://localhost:44307/api/Player`)
        .then((response) => {
            setAPIData(response.data);
        })
}, [])
    return (
        <div>
            <Table singleLine>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell>Name</Table.HeaderCell>

                    </Table.Row>
                </Table.Header>

                <Table.Body>
                {APIData.map((data) => {
     return (
       <Table.Row>
          <Table.Cell>{data.name}</Table.Cell>

           
        </Table.Row>
   )})}
                </Table.Body>
            </Table>
        </div>
    )
}
