import * as React from 'react';
import { useState } from 'react';
import { Box } from '@mui/system';
import Grid from '@mui/material/Grid';
import { Button, Collapse, Alert, IconButton } from '@mui/material';
import TextField from '@mui/material/TextField';
import axios from 'axios';
import { variables } from '../Variables';
import { Link, useLocation } from 'react-router-dom';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import CloseIcon from '@mui/icons-material/Close';


const DependantAlter = () => {
    const { state } = useLocation();
    const [newDependant, setDependant] = useState(state.dependant);
    const [successOpen, setSuccessOpen] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        console.log(e);
        setDependant({ ...newDependant, [name]: value });
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        alterDependants(state.type);
    }

    const alterDependants = async (type) => {
        if (type === "Add") {
            await axios.post(`${variables.API_URL}/Dependant`, {
                id: 0,
                employeeid: state.employee.id,
                firstName: newDependant.firstName,
                lastName: newDependant.lastName
            }).then(e => {
                setSuccessOpen(true);
            }).catch(function (error) {
                console.log(error);
            });;
        } else if (type === "Edit") {
            await axios.put(`${variables.API_URL}/Dependant/${state.dependant.id}`, {
                id: newDependant.id,
                employeeid: state.employee.id,
                firstName: newDependant.firstName,
                lastName: newDependant.lastName
            }).then(e => {
                setSuccessOpen(true);
            }).catch(function (error) {
                console.log(error);
            });
        }
    }

    return (

        <Box>
            <Collapse in={successOpen}>
                <Alert
                    action={
                        <IconButton
                            aria-label="close"
                            color="inherit"
                            size="small"
                            onClick={() => {
                                setSuccessOpen(false);
                            }}>
                            <CloseIcon fontSize="inherit" />
                        </IconButton>
                    }
                    sx={{ mb: 2 }}>
                    Sucessfully Submitted
                </Alert>
            </Collapse>
            <Grid item xs={12} sx={{ display: 'flex' }}>
                <div>
                    <Link to="/employees" ><Button startIcon={<ArrowBackIcon />} size="large">Go Back</Button></Link>
                </div>
            </Grid>
            <h1>{state.type} Dependant</h1>
            <form onSubmit={handleSubmit}>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <TextField
                            required
                            id="outlined-required"
                            label="First Name"
                            name="firstName"
                            value={newDependant.firstName}
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            required
                            id="outlined-required"
                            label="Last Name"
                            name="lastName"
                            value={newDependant.lastName}
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <Button variant="contained" color="success" type="submit">Submit</Button>
                    </Grid>
                </Grid>
            </form>
        </Box>
    );
}

export default DependantAlter;