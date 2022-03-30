import * as React from 'react';
import { useState } from 'react';
import { Box } from '@mui/system';
import Grid from '@mui/material/Grid';
import { Button } from '@mui/material';
import TextField from '@mui/material/TextField';
import { Link, useLocation } from 'react-router-dom';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import axios from 'axios';
import { variables } from '../Variables';
import Dependants from './Dependants';


const Employee = (props) => {
        const { state } = useLocation();
        const [employee, updateEmployee] = useState(state.employee ? state.employee : {});

        const handleChange = (e) => {
                const { name, value } = e.target;
                console.log(e);
                updateEmployee({ ...employee, [name]: value });
        }

        const handleSubmit = (e) => {
                e.preventDefault();
                editEmployee();
            }

        const editEmployee = async () => {
                await axios.put(`${variables.API_URL}/Employee/${employee.id}`, {
                        id: state.employee.id,
                        firstName: employee.firstName,
                        lastName: employee.lastName,
                        compensationType: state.employee.compensationType
                });
        }


        return (
                <div>
                        <Box
                                sx={{
                                        '& .MuiTextField-root': { m: 1, width: '25ch' },
                                        'flowGrow': 1
                                }}>
                                <Grid container spacing={2}>
                                        <Grid item xs={12} sx={{ display: 'flex' }}>
                                                <div>
                                                        <Link to="/employees"><Button startIcon={<ArrowBackIcon />} size="large">Go Back</Button></Link>
                                                </div>
                                        </Grid>
                                        <Grid item xs={6}>
                                                <form onSubmit={handleSubmit}>
                                                        <Grid item xs={12}>
                                                                <h1>Edit Employee</h1>
                                                                <TextField
                                                                        required
                                                                        id="outlined-required"
                                                                        label="First Name"
                                                                        name="firstName"
                                                                        defaultValue={state.employee.firstName}
                                                                        onChange={handleChange}
                                                                />
                                                                <TextField
                                                                        required
                                                                        id="outlined-required"
                                                                        label="Last Name"
                                                                        name="lastName"
                                                                        defaultValue={state.employee.lastName}
                                                                        onChange={handleChange}
                                                                />
                                                        </Grid>
                                                        <Grid item xs={12}>
                                                                <Button variant="contained" color="success" size='large' type="submit">Update</Button>
                                                        </Grid>
                                                </form>
                                        </Grid>
                                        <Grid item xs={6}>
                                                <Dependants employeeId={state.employee.id} />
                                        </Grid>
                                </Grid>
                        </Box>
                </div>
        );
}

export default Employee