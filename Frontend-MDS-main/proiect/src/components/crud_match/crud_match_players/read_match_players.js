import React , { useEffect,useState }from 'react';
import { Table,Button } from 'semantic-ui-react'
import axios from 'axios';
import { Link } from 'react-router-dom';
import { render } from 'react-dom';
import { contextsKey } from 'express-validator/src/base';


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



export default function Read_Match_Players() {

    const onDelete = (playerid) => {
        axios.delete(`https://localhost:44307/api/MatchPlayer/delete-by-PlayerId/${playerid}`)
    }

    const getData = () => {
        axios.get(`https://localhost:44307/api/MatchPlayer/get-by-MatchId?id=${localStorage.getItem("nr")}`)
            .then((getData) => {
                 setAPIData(getData.data);
             })
    }

        const functie = (playerid) => {
        onDelete(playerid);
        window.location.href = 'http://localhost:3000/Match_Players';

    }


    

    const [APIData, setAPIData] = useState([]);

useEffect(() => {
    axios.get(`https://localhost:44307/api/MatchPlayer/get-by-MatchId?id=${localStorage.getItem("nr")}`)
        .then((response) => {
            setAPIData(response.data);
            //console.log(response.data);
        })
}, [])



// const [D, setD] = useState([]);
// function fun(id){

//   console.log(id);
//   useEffect(()=>{axios.get(`https://localhost:44307/api/Player/get-by-id/${id}`)
//   .then((response) => {
//     console.log(response.data.name);
//     setD(old=>[...old,response.data.name])
// })},[])
//   return D;
// }

const { items, requestSort, sortConfig } = useSortableData(APIData);
    const getClassNamesFor = (matchid) => {
        if (!sortConfig) {
            return;
        }
        return sortConfig.key === matchid ? sortConfig.direction : undefined;
        };

    const setData = (data) => {
    let { playerid, matchId } = data;
    
    localStorage.setItem('Player ID', playerid);
    localStorage.setItem('Match ID', matchId);


    
}
    

    return (
        
            <Table singleLine className='tabel'>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('Player ID')}
              className={getClassNamesFor('Player ID')}
            >Player ID</button></Table.HeaderCell>
                        
                        
                        

                    </Table.Row>
                </Table.Header>

                <Table.Body>
                
                {items.map((data) => {
     return (
       <Table.Row>
          <Table.Cell key={data.playerId}>{data.playerId}</Table.Cell>
          {/* <Table.Cell >{fun(data.playerId)}</Table.Cell> */}

        </Table.Row>
   )})}
                </Table.Body>
            </Table>
        
        
    )
}
