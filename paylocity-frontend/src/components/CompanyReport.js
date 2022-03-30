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
import axios from 'axios';
import { variables } from '../Variables';

const CompanyReport = () => {
    const [employeeData, setData] = useState([]);
    
    useEffect(() => {
        getEmployeeData();
    }, []);

    const getEmployeeData = async () => {
        await axios
            .get(`${variables.API_URL}/Employee`)
            .then((response) => {
                setData(response.data);
            }).catch(function(error){
                console.log(error);
            });
    };
    
    return(
        <div>
            <Grid container spacing={2}>
                <Grid item xs={6}>
                    <Grid item xs={12}>
                        <h1>Paylocity Staff Expense Report</h1>
                    </Grid>
                    <Grid item xs={12}>
                        Total Employees: {employeeData.length}
                    </Grid>
                    <Grid item xs={12}>
                        Total Pre-reductions Payroll Cost: 
                        ${employeeData.length ? 
                            employeeData.map(emp => emp.pretaxedPaycheckSalary).reduce((a,b) => a+b) : 0 }
                    </Grid>
                    <Grid item xs={12}>
                        Total Post-reductions Payroll:
                        ${employeeData.length ? 
                            (employeeData.map(emp => emp.taxedPaycheckSalary).reduce((a,b) => a+b)).toFixed(2) : 0 }
                    </Grid>
                </Grid>
                <Grid item xs={6}>
                    <h1>Employee Breakdown</h1>
                    <TableContainer component={Paper}>
                        <Table sx={{ minWidth: 650 }} aria-label="simple table">
                            <TableHead>
                                <TableRow>
                                    <TableCell>First Name</TableCell>
                                    <TableCell>Last Name</TableCell>
                                    <TableCell>Paycheck Amount</TableCell>
                                    <TableCell>Net Amount</TableCell>
                                    <TableCell>Benefit Self-Cost</TableCell>
                                    <TableCell>Number of Dependants</TableCell>
                                    <TableCell>Dependant Cost</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {employeeData.length ? employeeData.map((row) => (
                                    <TableRow key={row.id}>
                                        <TableCell component="th" scope="row">{row.firstName}</TableCell>
                                        <TableCell>{row.lastName}</TableCell>
                                        <TableCell>{row.pretaxedPaycheckSalary}</TableCell>
                                        <TableCell>{row.taxedPaycheckSalary}</TableCell>
                                        <TableCell>{row.payCheckBenefitCost}</TableCell>
                                        <TableCell>{row.dependants.length}</TableCell>
                                        <TableCell>{(row.pretaxedPaycheckSalary-(row.taxedPaycheckSalary + row.payCheckBenefitCost)).toFixed(2)}</TableCell>
                                    </TableRow>
                                )) : <TableRow><TableCell>No Records found</TableCell></TableRow>}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Grid>
            </Grid>
        </div>
    );
}

export default CompanyReport;