import React , { useEffect,useState }from 'react';
import { Table,Button } from 'semantic-ui-react'
import axios from 'axios';
import { Link } from 'react-router-dom';
import './RP.css';


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
    //console.log(sortedItems)
    return { items: sortedItems, requestSort, sortConfig };
  };



export default function Read_Players() {

    const onDelete = (id) => {
        axios.delete(`https://localhost:44307/api/Player/delete-by-id/${id}`,{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
    }

    const getData = () => {
        axios.get(`https://localhost:44307/api/Player`)
            .then((getData) => {
                 setAPIData(getData.data);
             },{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
    }

        const functie = (id) => {
        onDelete(id);
        window.location.href = 'http://localhost:3000/Players';

    }



    const [APIData, setAPIData] = useState([]);
useEffect(() => {
    axios.get(`https://localhost:44307/api/Player/get-players`)
        .then((response) => {
            setAPIData(response.data);
        })
}, [])
const { items, requestSort, sortConfig } = useSortableData(APIData);
    const getClassNamesFor = (name) => {
        if (!sortConfig) {
            return;
        }
        return sortConfig.key === name ? sortConfig.direction : undefined;
        };

    const setData = (data) => {
    let { id, name, nationality, birth_Date,height,foot,position,value } = data;
    localStorage.setItem('ID', id);
    localStorage.setItem('Name', name);
    localStorage.setItem('Nationality', nationality);
    localStorage.setItem('Birth Date', birth_Date)
    localStorage.setItem('Height', height.toString().substring(0, (height.toString()).length-2));
    localStorage.setItem('Foot', foot);
    localStorage.setItem('Position', position);
    localStorage.setItem('Value', value)
}

    return (
        
            <Table striped className='tabel'>
              
                <Table.Header className='tt1'>
                    <Table.Row>
                    <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('id')}
              className={getClassNamesFor('id')}
            >ID</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('name')}
              className={getClassNamesFor('name')}
            >Name</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('nationality')}
              className={getClassNamesFor('nationality')}
            >Nationality</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('birth_Date')}
              className={getClassNamesFor('birth_Date')}
            >Birth Date</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('height')}
              className={getClassNamesFor('height')}
            >Height</button></Table.HeaderCell>
                        <Table.HeaderCell  className='titlu'><button
              type="button"
              onClick={() => requestSort('foot')}
              className={getClassNamesFor('foot')}
            >Foot</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('position')}
              className={getClassNamesFor('position')}
            >Position</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('value')}
              className={getClassNamesFor('value')}
            >Value</button></Table.HeaderCell>
                        

                    </Table.Row>
                </Table.Header>

                <Table.Body>
                {/* {APIData.map((data) => { */}
                {items.map((data) => {
     return (
       <Table.Row>
          <Table.Cell key={data.id}>{data.id}</Table.Cell>
          <Table.Cell >{data.name}</Table.Cell>
          <Table.Cell >{data.nationality}</Table.Cell>
          <Table.Cell >{data.birth_Date}</Table.Cell>
          <Table.Cell >{data.height}</Table.Cell>
          <Table.Cell >{data.foot}</Table.Cell>
          <Table.Cell >{data.position}</Table.Cell>
          <Table.Cell >{data.value*1000000}</Table.Cell>

           <Link to='/update_players'>
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
            </Table>
        
        
    )
}
