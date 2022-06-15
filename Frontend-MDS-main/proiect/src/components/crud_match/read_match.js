import React , { useEffect,useState }from 'react';
import { Table,Button } from 'semantic-ui-react'
import axios from 'axios';
import { Link } from 'react-router-dom';
import Read_Match_Players from './crud_match_players/read_match_players';



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



export default function Read_Match() {

    const onDelete = (matchId) => {
        axios.delete(`https://localhost:44307/api/Match/delete-by-id/${matchId}`,{headers: { 'Content-Type': 'application/json', 'charset':'utf-8', 'Authorization': `Bearer ${localStorage.getItem("jwt").replaceAll("\"","")}`}})
    }

    const getData = () => {
        axios.get(`https://localhost:44307/api/Match/get-all-matches`)
            .then((getData) => {
                 setAPIData(getData.data);
             })
    }

        const functie = (matchId) => {
        onDelete(matchId);
        window.location.href = 'http://localhost:3000/Matches';

    }
  
    const fct = (matchId)=>{
      //link catre match players si match staff
      //Read_Match_Players.getData(matchId);
      //window.location.href=`https://localhost:44307/api/MatchPlayer/get-by-MatchId?id=${matchId}`;
      localStorage.setItem("nr",matchId);
    }



    const [APIData, setAPIData] = useState([]);
useEffect(() => {
    axios.get(`https://localhost:44307/api/Match/get-all-matches`)
        .then((response) => {
            //console.log(response.data);
            setAPIData(response.data);
        })
}, [])
const { items, requestSort, sortConfig } = useSortableData(APIData);
    const getClassNamesFor = (competition) => {
        if (!sortConfig) {
            return;
        }
        return sortConfig.key === competition ? sortConfig.direction : undefined;
        };

    const setData = (data) => {
    let { matchId, stadiumId, opponent, competition, event_date, score, referee, id } = data;
    localStorage.setItem('Match ID', matchId);
    localStorage.setItem('Stadium ID', stadiumId);
    localStorage.setItem('Opponent', opponent);
    localStorage.setItem('Competition', competition);
    localStorage.setItem('Event Date', event_date);
    localStorage.setItem('Score', score);
    localStorage.setItem('Referee', referee);
    localStorage.setItem('Match ID', id);
}

    return (
        
            <Table singleLine className='tabel'>
                <Table.Header className='tt1'>
                    <Table.Row>
                    <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('matchId')}
              className={getClassNamesFor('matchId')}
            >id</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('Stadium ID')}
              className={getClassNamesFor('Stadium ID')}
            >Stadium ID</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('Opponent')}
              className={getClassNamesFor('Opponent')}
            >Opponent</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('Competition')}
              className={getClassNamesFor('Competition')}
            >Competition</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('Event Date')}
              className={getClassNamesFor('Event Date')}
            >Event Date</button></Table.HeaderCell>
                        <Table.HeaderCell  className='titlu'><button
              type="button"
              onClick={() => requestSort('Score')}
              className={getClassNamesFor('Score')}
            >Score</button></Table.HeaderCell>
                        <Table.HeaderCell className='titlu'><button
              type="button"
              onClick={() => requestSort('Referee')}
              className={getClassNamesFor('Referee')}
            >Referee</button></Table.HeaderCell>
                        
                        

                    </Table.Row>
                </Table.Header>

                <Table.Body>

                {items.map((data) => {
     return (
       <Table.Row>
          <Table.Cell key={data.id}>{data.id}</Table.Cell>
          <Table.Cell >{data.stadiumId}</Table.Cell>
          <Table.Cell >{data.opponent}</Table.Cell>
          <Table.Cell >{data.competition}</Table.Cell>
          <Table.Cell >{data.event_date}</Table.Cell>
          <Table.Cell >{data.score}</Table.Cell>
          <Table.Cell >{data.referee}</Table.Cell>

           <Link to='/update_match'>
          <Table.Cell> 
        <Button onClick={() => setData(data)}>Update</Button>
        </Table.Cell>
        </Link> 
        <Table.Cell>
        <Button onClick={() => functie(data.id)}>Delete</Button>
        </Table.Cell>
        <Link to='/mplayers'><Table.Cell>
        <Button onClick={() => fct(data.id)}>Match Details</Button>
        {/* <Button onClick={() => console.log(data.id)}>Match Details</Button> */}
        </Table.Cell></Link> 
        
        </Table.Row>
   )})}
                </Table.Body>
            </Table>
        
        
    )
}
