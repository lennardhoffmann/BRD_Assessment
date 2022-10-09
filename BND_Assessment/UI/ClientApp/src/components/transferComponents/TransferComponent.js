import React, { useEffect, useState } from "react";
import { FormControl, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import { useSelector } from "react-redux";

import './_style.transferComponent.scss';

export default _=>{
    const customerList = useSelector(s=>s.customers).customers;

    const[state,setState]=useState({
sourceCustomerList: null,
targetCustomerList:null,
sourceCustomer:null,
targetCustomer:null,
amount: 0,
amountError: false,
maxAmount:null
    });

    const {sourceCustomer,targetCustomer, sourceCustomerList, targetCustomerList,amount,amountError, maxAmount} = state;

    useEffect(_=>{
        if(customerList){
            if(!sourceCustomerList)
            setState({...state,sourceCustomerList: customerList})
        }
    })

    const handleSourceSelect = sourceId=>{
        var filteredArray = [...sourceCustomerList];
        var index = filteredArray.findIndex(c=> c.id == sourceId);
        let max = filteredArray[index].balance

        filteredArray.splice(index, 1);        

setState({...state, sourceCustomer: sourceId, targetCustomerList: filteredArray,maxAmount: max});
    }

    const handleTargetSelect = targetId=>{
        setState({...state, targetCustomer: targetId})
    }

    const handleAmountChange = amt=>{
        if(amt && (amt < 1 || amt > maxAmount)){
            setState({...state, amountError: true, amount: 0});
            return;
        }

        setState({...state, amount: amt, amountError: false})       
    }

    return(
        <div style={{display: 'flex',flexDirection: 'column', width:'60%', height:'70%', alignSelf: 'center',backgroundColor: 'seashell', border: '1px solid grey',borderRadius:'1vh', padding: '2vh'}}>
            <label style={{fontSize:'2vh', marginBottom:'2vh',fontWeight:'bold'}}>Transfer amount between customer accounts</label>
            <FormControl fullWidth>
            <InputLabel id="select-customer-label">Source customer account</InputLabel>
            <Select 
            value={sourceCustomer || ""} 
            labelId="select-customer-label"
            label="Source customer account"
            onChange={e=> handleSourceSelect(e.target.value)}
            >
{ sourceCustomerList && sourceCustomerList.length > 0 && sourceCustomerList.map((customer,i)=>{
    return <MenuItem key={`key-source-${i}-${customer.accountNumber}`} value={customer.id}>{`${customer.firstName} ${customer.lastName} | ${customer.iban}`}</MenuItem>
})}
            </Select>
            </FormControl>
<div style={{marginBottom: '2vh'}}/>
            <FormControl fullWidth>
            <InputLabel id="select-customer-label">Target customer account</InputLabel>
            <Select 
            value={targetCustomer || ""} 
            labelId="select-customer-label"
            label="Target customer account"
            onChange={e=> handleTargetSelect(e.target.value)}
            disabled={!sourceCustomer&& true}
            >
{ targetCustomerList && targetCustomerList.length > 0 && targetCustomerList.map((customer,i)=>{
    return <MenuItem key={`key-target-${i}-${customer.accountNumber}`} value={customer.id}>{`${customer.firstName} ${customer.lastName} | ${customer.iban}`}</MenuItem>
})}
            </Select>
            </FormControl>
            <TextField 
            disabled={!sourceCustomer && !targetCustomer && true}
            error={amountError}
            helperText={amountError ? `Please specify an amount greater than 0 and less than ${maxAmount}` : ''}
            variant="outlined" 
            label="Amount" 
            value={amount} 
            type="number"
            onChange={e=> handleAmountChange(e.target.value)}
            style={{marginTop: '2vh', width: '40%'}}/>
            <label style={{fontStyle: 'italic'}}>{maxAmount && `Please not the maximum aount available for transfer is limited to ${maxAmount}. This is the balance of the source account`}</label>
        </div>
    )
}