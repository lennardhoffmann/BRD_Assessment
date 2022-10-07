import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { CustomerAccountService } from "../../services";
import { DataGrid } from '@mui/x-data-grid';
import { Button, Modal } from "@mui/material";
import { setActiveCustomer, toggleModal } from "../../state/stateFeatures/customerSlice";
import CustomerDetailComponent from "./CustomerDetailComponent";
import CloseIcon from '@mui/icons-material/Close';

export default _ =>{
    const customerState = useSelector(s=> s.customers);
    const dispatch = useDispatch();
   
    useEffect(_=>{
        if(!customerState.customers){
            CustomerAccountService.GetCustomerAccounts();
        }
    });

    const columns = [
        {
            field: "firstName",
            flex: 1,
            renderHeader: () =>(
                <label>Customer Name</label>
            ),
            renderCell: (cellValues)=>(
                <label>{`${cellValues.row.firstName} ${cellValues.row.lastName}`}</label>
            )
        },
        {
            field: "iban",
            flex: 1,
            renderHeader: () =>(
                <label>Account number</label>
            ),
            renderCell: (cellValues)=>(
                <label>{cellValues.row.iban}</label>
            )
        },
        {
            field: "balance",
            flex: 1,
            renderHeader: () =>(
                <label>Account balance</label>
            ),
            renderCell: (cellValues)=>(
                <label>{cellValues.row.balance}</label>
            )
        },
        {
            field: "id",
            flex: 1,
            headerName: "",
            renderCell: (cellValues)=>(
                <div style={{display: 'flex', justifyContent: 'center', width: '100%'}}>
                    <Button variant="contained" 
                    onClick={_ => {
                        dispatch(setActiveCustomer(cellValues.row));
                        dispatch(toggleModal(true));
                        }}>Edit customer</Button>
                </div>
            )
        }
    ];

    return(
        <div style={{display: 'flex', flexDirection: 'column', width: '100%', height:'100%',padding: '2vh', backgroundColor: 'lightyellow'}}>
            <Modal open={customerState.showModal} style={{display:'flex',flexDirection: 'column', justifyContent: 'center'}}>
                <div style={{display: 'flex',flexDirection: 'column',width: '60%',height: '60%', backgroundColor: 'white', alignSelf: 'center'}}>
<CloseIcon style={{alignSelf: 'flex-end'}} fontSize="large" onClick={_=> dispatch(toggleModal(false))}/>
                    <CustomerDetailComponent/>
                </div>
            </Modal>
            <label style={{fontWeight: 'bold',fontSize:'2vh'}}>Customers</label>
            { customerState.customers && <div style={{width:'100%', height:'60%'}}>
                <DataGrid
                columns={columns}
                checkboxSelection={false}
                rows={customerState.customers}
                />
            </div>
            }
            <div style={{display: 'flex',alignSelf: 'center', marginTop: '2vh'}}>
                <Button variant="contained" 
                onClick={_=>{
                    dispatch(toggleModal(true));
                }}>
                    Add Customer
                    </Button>
            </div>
        </div>
    )
}