import React , { useEffect,useState }from 'react';
import { Table,Button } from 'semantic-ui-react'
import axios from 'axios';
import { Link } from 'react-router-dom';
//import './RS.css';


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



export default function Read_Stadium() {

    const onDelete = (stadiumId) => {
        axios.delete(`https://localhost:44307/api/Stadium/delete-by-id/${stadiumId}`,{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
    }

    const getData = () => {
        //metoda get stadiums
        axios.get(`https://localhost:44307/api/Stadium/get-by-name`)
            .then((getData) => {
                 setAPIData(getData.data);
             })
    }

        const functie = (stadiumId) => {
        onDelete(stadiumId);
        window.location.href = 'http://localhost:3000/Stadiums';

    }



    const [APIData, setAPIData] = useState([]);
useEffect(() => {
    //nu e metoda
    axios.get(`https://localhost:44307/api/Stadium/get-stadiums`)
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
    let { stadiumId, name, capacity, surface, address, id } = data;
    localStorage.setItem('Stadium ID', stadiumId);
    localStorage.setItem('Name', name);
    localStorage.setItem('Capacity', capacity);
    localStorage.setItem('Surface', surface);
    localStorage.setItem('Address', address);
    localStorage.setItem('Stadium ID', id);
}

    return (
        
            <Table singleLine className='tabel'>
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
              onClick={() => requestSort('capacity')}
              className={getClassNamesFor('capacity')}
            >Capacity</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('surface')}
              className={getClassNamesFor('surface')}
            >Surface</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('address')}
              className={getClassNamesFor('address')}
            >Address</button></Table.HeaderCell>
                        
                        

                    </Table.Row>
                </Table.Header>

                <Table.Body>
                
                {items.map((data) => {
     return (
       <Table.Row>
        <Table.Cell>{data.id}</Table.Cell>
          <Table.Cell key={data.name}>{data.name}</Table.Cell>
          <Table.Cell >{data.capacity}</Table.Cell>
          <Table.Cell >{data.surface}</Table.Cell>
          <Table.Cell >{data.address}</Table.Cell>
          
           <Link to='/update_stadium'>
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
