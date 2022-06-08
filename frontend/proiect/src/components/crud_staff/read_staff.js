import React, {useEffect, useState} from "react";
import {Table, Button} from 'semantic-ui-react';
import axios from 'axios';
import {Link} from 'react-router-dom';
import './RS.css';

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

  export default function Read_Staff(){
      const onDelete = (id) => {
          axios.delete(`https://localhost:44307/api/StaffMember/delete-by-id/{id}${id}`)
      }

      const getData = () => {
          axios.get(`https://localhost:44307/api/StaffMember`).then((getData)=>{
              setAPIData(getData.data);
          })
      }

      const functie = (id) =>{
          onDelete(id);
          window.location.href = 'http://localhost:3000/StaffMember';
      }

      const [APIData, setAPIData] = useState([]);
      useEffect(() => {
          axios.get(`https://localhost:44307/api/StaffMember`).then((response) => {
              setAPIData(response.data);
          })
      }, [])

      const {items, requestSort, sortConfig} = useSortableData(APIData);
      const getClassNamesFor = (name) => {
          if(!sortConfig){
              return;
          }
          return sortConfig.key === name ? sortConfig.direction : undefined;
      };

      const setData = (data) => {
          let {id, name, role, birth_date, email, phone_number} = data;
          localStorage.setItem('ID', id);
          localStorage.setItem('Name', name);
          localStorage.setItem('Role', role);
          localStorage.setItem('Birth Date', birth_date);
          localStorage.setItem('Email', email);
          localStorage.setItem('Phone Number', phone_number);
      }

      return(
          <Table singleLine className='tabel'>
              <Table.Header>
                  <Table.Row>
                      <Table.HeaderCell className='titlu'>
                          <button type="button" 
                          onClick={() => requestSort('name')}
                          className={getClassNamesFor('name')}>
                            Name
                          </button>
                      </Table.HeaderCell>
                      <Table.HeaderCell className='titlu'>
                          <button type="button" 
                          onClick={() => requestSort('role')}
                          className={getClassNamesFor('role')}>
                            Role
                          </button>
                      </Table.HeaderCell>
                      <Table.HeaderCell className='titlu'>
                          <button type="button" 
                          onClick={() => requestSort('birth_date')}
                          className={getClassNamesFor('birth_date')}>
                            Birth Date
                          </button>
                      </Table.HeaderCell>
                      <Table.HeaderCell className='titlu'>
                          <button type="button" 
                          onClick={() => requestSort('email')}
                          className={getClassNamesFor('email')}>
                            Email
                          </button>
                      </Table.HeaderCell>
                      <Table.HeaderCell className='titlu'>
                          <button type="button" 
                          onClick={() => requestSort('phone_number')}
                          className={getClassNamesFor('phone_number')}>
                            Phone Number
                          </button>
                      </Table.HeaderCell>
                  </Table.Row>
              </Table.Header>
              
              <Table.Body>
                  {items.map((data) => {
                      return(
                          <Table.Row>
                              <Table.Cell key={data.name}>{data.name}</Table.Cell>
                              <Table.Cell>{data.role}</Table.Cell>
                              <Table.Cell>{data.birth_date}</Table.Cell>
                              <Table.Cell>{data.email}</Table.Cell>
                              <Table.Cell>{data.phone_number}</Table.Cell>

                              <Link to='update staff'>
                                  <Table.Cell>
                                      <Button onClick = {() => setData(data)}>Update</Button>
                                  </Table.Cell>
                              </Link>
                              <Table.Cell>
                                  <Button onClick = {() => functie(data.id)}>Delete</Button>
                              </Table.Cell>
                          </Table.Row>
                      )
                  })}
              </Table.Body>
          </Table>
      )
  }