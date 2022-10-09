import { Button, FormControl, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { CustomerAccountService, DepositService } from "../../services";
import { store } from "../../state";
import { showSnackbar, toggleLoadScreen } from "../../state/stateFeatures/navigationSlice";

import './_style.depositComponent.scss'

export default _=>{
    const dispatch = useDispatch();
    const[state,setState] = useState({
        amount:0,
        selectedCustomer: null,
        customers: [],
        amountError: false
    });

    const {amount,selectedCustomer,customers,amountError} = state;

    useEffect(_=>{
var customerList = store.getState().customers.customers;

if(!customerList){
    CustomerAccountService.GetCustomerAccounts()
    .then(_=>{
        customerList = store.getState().customers.customers;

        if(customerList && customerList.length>0){
            setState({...state,customers:customerList});
        }
    })
}
else{
    setState({...state, customers:customerList});
}
    });

    const handleSelect = value =>{
        console.log('val', value)
        setState({...state,selectedCustomer:value})
    }

    const handleAmountChange = amt=>{
        if(amt && amt < 1){
            setState({...state, amountError: true});
            return;
        }

        setState({...state, amount: amt, amountError: false})       
    }

    const handleDeposit=_=>{
        dispatch(toggleLoadScreen(true));

var depositObj = {'customerAccountId': selectedCustomer, 'depositAmount': amount};

DepositService.MakeCustomerDeposit(depositObj)
.then(_=>{
    setTimeout(() => {                               
        dispatch(toggleLoadScreen(false))
        dispatch(showSnackbar({show: true, message: `An amount of ${amount * 0.999} has successfully been deposited to the customer account`}));
    }, 1000);
})

setState({selectedCustomer: null, amount:0, amountError: false, customers: []});
    }

    return<div className="mainDepositContainerBox">
        <div className="depositBox">
            <label className="depositLabel">Deposit an amount to a customer account</label>
            <FormControl fullWidth>
            <InputLabel id="select-customer-label">Customer account</InputLabel>
            <Select 
            value={selectedCustomer || ""} 
            labelId="select-customer-label"
            label="Customer account"
            onChange={e=> handleSelect(e.target.value)}
            placeholder="Please select an account"
            >
{customers.length > 0 && customers.map((customer,i)=>{
    return <MenuItem key={`key-${i}-${customer.accountNumber}`} value={customer.id}>{`${customer.firstName} ${customer.lastName} | ${customer.iban}`}</MenuItem>
})}
            </Select>
            </FormControl>
            <TextField 
            error={amountError}
            helperText={amountError ? 'Please specify an amount greater than 0' : ''}
            variant="outlined" 
            label="Amount" 
            value={amount} 
            type="number"
            onChange={e=> handleAmountChange(e.target.value)}
            style={{marginTop: '2vh', width: '40%'}}/>
            <label style={{fontStyle:'italic'}}>Please note that a service charge of 0.1% will be charged</label>
            <Button 
            disabled={selectedCustomer && amount > 0 ? false : true}
            variant="contained"
            onClick={_=> handleDeposit()}
            style={{marginTop:'3vh',width: '20%',alignSelf: 'center'}}
            >Deposit amount</Button>
        </div>
    </div>
}