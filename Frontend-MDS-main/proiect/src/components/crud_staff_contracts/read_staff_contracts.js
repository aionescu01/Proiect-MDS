import React, { useEffect, useState } from "react";
import { Table,Button } from "semantic-ui-react";
import {Link} from 'react-router-dom';
import axios from "axios";
//import './RP.css';

const useSortableData = (items, config = null) => {
    const [sortConfig, setSortConfig] = React.useState(config);
  
    const sortedItems = React.useMemo(() => {
      let sortableItems = [...items];
      if (sortConfig !== null) {
        sortableItems.sort((a, b) => {
          if (a[sortConfig.key] < b[sortConfig.key]) {
            return sortConfig.direction === 'ascending' ? -1 : 1;
          }
          if (a[sortConfig.key] > b[sortConfig.key]) {
            return sortConfig.direction === 'ascending' ? 1 : -1;
          }
          return 0;
        });
      }
      return sortableItems;
    }, [items, sortConfig]);
  
    const requestSort = (key) => {
      let direction = 'ascending';
      if (
        sortConfig &&
        sortConfig.key === key &&
        sortConfig.direction === 'ascending'
      ) {
        direction = 'descending';
      }
      setSortConfig({ key, direction });
    };
    
    return { items: sortedItems, requestSort, sortConfig };
  };


export default function Read_Staff_Contracts(){
    const [APIData, setAPIData] = useState([]);
    useEffect(() => {
        axios.get('https://localhost:44307/api/Contract/get-all-staff-contracts').then((response) => {
            setAPIData(response.data);
        })
    }, [])

    const onDelete = (staffmemberId) => {
        axios.delete(`https://localhost:44307/api/Contract/delete-by-id/${staffmemberId}`,{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
    }
    const functie = (staffmemberId) => {
        onDelete(staffmemberId);
        window.location.href = 'http://localhost:3000/Staff_Contracts';

    }

    const { items, requestSort, sortConfig } = useSortableData(APIData);
    const getClassNamesFor = (name) => {
        if (!sortConfig) {
            return;
        }
        return sortConfig.key === name ? sortConfig.direction : undefined;
        };

    const setData = (data) => {
    let {staffMemberId,name, start_date, end_date, salary, agent, id } = data;
    localStorage.setItem('Name', name);
    localStorage.setItem('Staff Member ID', staffMemberId);
    localStorage.setItem('Start Date', start_date);
    localStorage.setItem('End Date', end_date);
    localStorage.setItem('Salary', salary);
    localStorage.setItem('Agent', agent);
    localStorage.setItem('Contract ID',id);
}

    return (
        
            <Table striped className='tabel'>
                <Table.Header className='tt1'>
                    <Table.Row>
                    <Table.HeaderCell className='titlu'>
                          <button type="button" 
                          onClick={() => requestSort('name')}
                          className={getClassNamesFor('name')}>
                            Name
                          </button>
                      </Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('start_date')}
              className={getClassNamesFor('start_date')}
            >Start Date</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('end_date')}
              className={getClassNamesFor('end_date')}
            >End Date</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('salary')}
              className={getClassNamesFor('salary')}
            >Salary</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('agent')}
              className={getClassNamesFor('agent')}
            >Agent</button></Table.HeaderCell>
                        
                        

                    </Table.Row>
                </Table.Header>

                <Table.Body>

                {items.map((data) => {
     return (
       <Table.Row>
           <Table.Cell key={data.name}>{data.name}</Table.Cell>
          <Table.Cell >{data.start_date}</Table.Cell>
          <Table.Cell >{data.end_date}</Table.Cell>
          <Table.Cell >{data.salary}</Table.Cell>
          <Table.Cell >{data.agent}</Table.Cell>
          
           <Link to='/update_staff_contracts'>
          <Table.Cell> 
        <Button onClick={() => setData(data)}>Update</Button>
        </Table.Cell>
        </Link> 
         <Table.Cell>
        <Button onClick={() => functie(data.id)}>Delete</Button>
        </Table.Cell> 

        </Table.Row>
   )})}
                </Table.Body>

            </Table>)
}
