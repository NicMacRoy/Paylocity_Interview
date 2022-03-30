import { Link } from 'react-router-dom';
import { useState, useEffect } from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import EditIcon from '@mui/icons-material/Edit';
import { variables } from '../Variables';
import { Tooltip } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import AddCircleIcon from '@mui/icons-material/AddCircle';
import { Button } from '@mui/material';
import { Grid } from '@mui/material';
import axios from 'axios';

const Employees = () => {
    const [employeeData, setData] = useState([]);

    useEffect(() => {
        getEmployeeData();
    }, []);

    const deleteEmployee = async (targetedId) => {
        await axios.delete(`${variables.API_URL}/Employee/${targetedId}`)
            .catch(function (error) {
                console.log(error.response);
            });

        getEmployeeData();
    }

    const getEmployeeData = async () => {

        await axios.get(`${variables.API_URL}/Employee`)
            .then((response) => {
                setData(response.data);
            }).catch(function (error) {
                console.log(error.response);
            });
    };

    return (
        <div>
            <Grid item xs={12}>
                <h2>Employees <Link to="/addemployee" state={{ employee: { id: 0, firstName: "", lastName: "", relationship: 0 }, type: "Add" }}><Tooltip title="Add Employee"><Button sx={{ float: 'right', marginRight: "20px" }}><AddCircleIcon color="success" fontSize='large' /></Button></Tooltip></Link> </h2>
            </Grid>
            <TableContainer component={Paper} style={{'margin-left': 20}}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>First Name</TableCell>
                            <TableCell>Last Name</TableCell>
                            <TableCell>Number of Dependants</TableCell>
                            <TableCell>Options</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {employeeData.length ? employeeData.map((row) => (
                            <TableRow
                                key={row.id}
                            >
                                <TableCell component="th" scope="row">
                                {row.firstName}
                                </TableCell>
                                <TableCell>{row.lastName}</TableCell>
                                <TableCell>{row.dependants.length}</TableCell>
                                <TableCell><Tooltip title="Edit Employee"><Link to="/employee" state={{employee: row}}><EditIcon fontSize='large' /></Link></Tooltip><Tooltip title="Delete Employee"><Button onClick={() => deleteEmployee(row.id)}><DeleteIcon fontSize='large' /></Button></Tooltip></TableCell>
                            </TableRow>
                        )) : <TableRow><TableCell>No Records found</TableCell></TableRow>}
                    </TableBody>
                </Table>
            </TableContainer>
        </div>
    );
}

export default Employees