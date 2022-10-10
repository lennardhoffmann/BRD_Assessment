import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { CustomerAccountService } from "../../services";
import { DataGrid } from '@mui/x-data-grid';
import { Button, Modal } from "@mui/material";
import { setActiveCustomer, toggleModal } from "../../state/stateFeatures/customerSlice";
import CustomerDetailComponent from "./CustomerDetailComponent";
import CloseIcon from '@mui/icons-material/Close';
import { toggleLoadScreen } from "../../state/stateFeatures/navigationSlice";

import './_style.customersComponent.scss';

export default _ => {
    const customerState = useSelector(s => s.customers);
    const dispatch = useDispatch();

    useEffect(_ => {
        if (!customerState.customers) {
            dispatch(toggleLoadScreen(true));

            CustomerAccountService.GetCustomerAccounts()
                .then(_ => {
                    setTimeout(() => {
                        dispatch(toggleLoadScreen(false))
                    }, 1500);
                })
        }

        setTimeout(() => {
            dispatch(toggleLoadScreen(false))
        }, 1500);
    });

    const HandleModalClose = _ => {
        dispatch(setActiveCustomer(null));
        dispatch(toggleModal(false))
    }

    const columns = [
        {
            field: "firstName",
            flex: 1,
            renderHeader: () => (
                <label style={{ fontWeight: 'bold' }}>Customer Name</label>
            ),
            renderCell: (cellValues) => (
                <label>{`${cellValues.row.firstName} ${cellValues.row.lastName}`}</label>
            )
        },
        {
            field: "iban",
            flex: 1,
            renderHeader: () => (
                <label style={{ fontWeight: 'bold' }}>Account number</label>
            ),
            renderCell: (cellValues) => (
                <label>{cellValues.row.iban}</label>
            )
        },
        {
            field: "balance",
            flex: 1,
            renderHeader: () => (
                <label style={{ fontWeight: 'bold' }}>Account balance</label>
            ),
            renderCell: (cellValues) => (
                <label>{cellValues.row.balance.toFixed(2)}</label>
            )
        },
        {
            field: "id",
            flex: 1,
            headerName: "",
            renderCell: (cellValues) => (
                <div style={{ display: 'flex', justifyContent: 'center', width: '100%' }}>
                    <Button variant="contained"
                        onClick={_ => {
                            dispatch(setActiveCustomer(cellValues.row));
                            dispatch(toggleModal(true));
                        }}>View Account</Button>
                </div>
            )
        }
    ];

    return (
        <div className="customerBox">
            <Modal open={customerState.showModal} className="modalBox">
                <div className="modalDisplay">
                    <CloseIcon style={{ alignSelf: 'flex-end', cursor: "pointer" }} fontSize="large" onClick={_ => HandleModalClose()} />
                    <CustomerDetailComponent />
                </div>
            </Modal>
            <label style={{ fontWeight: 'bold', fontSize: '2vh' }}>Customers</label>
            {customerState.customers &&
                <>
                    <div style={{ width: '100%', height: '60%' }}>
                        <DataGrid
                            columns={columns}
                            checkboxSelection={false}
                            rows={customerState.customers}
                            rowsPerPageOptions={[5, 10, 20]}
                        />
                    </div>

                    <Button variant="contained"
                        style={{ alignSelf: 'center', marginTop: '2vh' }}
                        onClick={_ => {
                            dispatch(toggleModal(true));
                        }}>
                        Add Customer
                    </Button>
                </>
            }
        </div>
    )
}