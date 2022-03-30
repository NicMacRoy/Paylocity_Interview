import * as React from 'react';
import { useState, useEffect } from 'react';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import DeleteIcon from '@mui/icons-material/Delete';
import AddCircleIcon from '@mui/icons-material/AddCircle';
import EditIcon from '@mui/icons-material/Edit';
import { Button } from '@mui/material';
import { Tooltip } from '@mui/material';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { variables } from '../Variables';
import { useLocation } from 'react-router-dom';

const Dependants = () => {
    const { state } = useLocation();
    const [dependantData, setData] = useState([]);

    useEffect(() => {
        getDependantData();
    }, []);

    const getDependantData = async () => {

        await axios
            .get(`${variables.API_URL}/Dependant/GetList/${state.employee.id}`)
            .then((response) => {

                setData(response.data);
            }).catch(function (error) {
                console.log(error.response);
            });
    };

    const DeleteDependant = async (targetedId) => {
        await axios.delete(`${variables.API_URL}/Dependant/${targetedId}`)
            .catch(function (error) {
                console.log(error.response);
            });

        getDependantData();
    }

    return (
        <div>
            <Grid item xs={12}>
                <h2>Dependants <Link to="/dependant" state={{ dependant: { id: 0, firstName: "", lastName: "", relationship: 0 }, employee: state.employee, type: "Add" }}><Tooltip title="Add Dependant"><Button sx={{ float: 'right', marginRight: "20px" }}><AddCircleIcon color="success" fontSize='large' /></Button></Tooltip></Link> </h2>
            </Grid>
            <Grid item xs={12}>
                <TableContainer component={Paper}>
                    <Table sx={{ minWidth: 650 }} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell>First Name</TableCell>
                                <TableCell>Last Name</TableCell>
                                <TableCell>Relationship</TableCell>
                                <TableCell>Options</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {dependantData.length ? dependantData.map((row) => (
                                <TableRow key={row.id}>
                                    <TableCell component="th" scope="row">{row.firstName}</TableCell>
                                    <TableCell>{row.lastName}</TableCell>
                                    <TableCell>{row.relationship}</TableCell>
                                    <TableCell><Link to="/dependant" state={{ dependant: row, employee: state.employee, type: "Edit" }}><Tooltip title="Edit Dependant"><EditIcon /></Tooltip></Link><Tooltip title="Delete Dependant"><Button onClick={() => DeleteDependant(row.id)}><DeleteIcon /></Button></Tooltip></TableCell>
                                </TableRow>
                            )) : <TableRow><TableCell>No Records found</TableCell></TableRow>}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>
        </div>
    )
}

export default Dependants;