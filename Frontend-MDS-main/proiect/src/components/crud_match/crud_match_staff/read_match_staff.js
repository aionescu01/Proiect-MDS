import React , { useEffect,useState }from 'react';
import { Table,Button } from 'semantic-ui-react'
import axios from 'axios';
import { Link } from 'react-router-dom';



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



export default function Read_Match_Staff() {

    const onDelete = (staffmemberid) => {
        axios.delete(`https://localhost:44307/api/MatchStaff/delete-by-StaffMemberId/${staffmemberid}`)
    }

    const getData = () => {
        axios.get(`https://localhost:44307/api/MatchStaff/get-all-matchstaff`)
            .then((getData) => {
                 setAPIData(getData.data);
             })
    }

        const functie = (staffmemberid) => {
        onDelete(staffmemberid);
        window.location.href = 'http://localhost:3000/Match_Staff';

    }



    const [APIData, setAPIData] = useState([]);
useEffect(() => {
    axios.get(`https://localhost:44307/api/MatchStaff/get-all-matchstaff`)
        .then((response) => {
            setAPIData(response.data);
        })
}, [])
const { items, requestSort, sortConfig } = useSortableData(APIData);
    const getClassNamesFor = (matchid) => {
        if (!sortConfig) {
            return;
        }
        return sortConfig.key === matchid ? sortConfig.direction : undefined;
        };

    const setData = (data) => {
    let { staffmemberid, matchid } = data;
    localStorage.setItem('Staff ID', staffmemberid);
    localStorage.setItem('Match ID', matchid);
    
}

    return (
        
            <Table singleLine className='tabel'>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('Staff ID')}
              className={getClassNamesFor('Staff ID')}
            >Staff ID</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('Match ID')}
              className={getClassNamesFor('Match ID')}
            >Match ID</button></Table.HeaderCell>
                        
                        

                    </Table.Row>
                </Table.Header>

                <Table.Body>
                
                {items.map((data) => {
     return (
       <Table.Row>
          <Table.Cell key={data.matchid}>{data.matchid}</Table.Cell>
          <Table.Cell >{data.staffmemberid}</Table.Cell>
          
           <Link to='/update_match_staff'>
          <Table.Cell> 
        <Button onClick={() => setData(data)}>Update</Button>
        </Table.Cell>
        </Link> 
         <Table.Cell>
        <Button onClick={() => functie(data.staffmemberid)}>Delete</Button>
        </Table.Cell> 

        </Table.Row>
   )})}
                </Table.Body>
            </Table>
        
        
    )
}
