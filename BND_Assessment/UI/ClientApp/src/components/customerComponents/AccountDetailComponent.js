import React, { useEffect, useState } from "react";
import {  Button,  TextField } from "@mui/material";
import { useDispatch } from "react-redux";
import { setActiveCustomer, toggleModal } from "../../state/stateFeatures/customerSlice";
import { CustomerAccountService } from "../../services";
import { showSnackbar, toggleLoadScreen } from "../../state/stateFeatures/navigationSlice";

import './_style.accountDetailComponent.scss';

export default props =>{
    const dispatch = useDispatch();
    const [state,setState] = useState({
        firstName: null,
        lastName: null,
        email: null,
        iban: null,
        balance: 0,
        id: 0,
        accountNumber: null
        });

    const {firstName,lastName, email,iban, balance, accountNumber} = state;

    
        useEffect(_=>{
            if(props.customerDetails && !firstName && !lastName && !email && !iban && !balance){
    setState({
        firstName: props.customerDetails.firstName,
        lastName:props.customerDetails.lastName,
        email:props.customerDetails.email,
        iban:props.customerDetails.iban,
        balance:props.customerDetails.balance,
        accountNumber: props.customerDetails.accountNumber,
        id: props.customerDetails.id
    })
            }
        })

      const HandleChange= (prop,value)=>{
        setState({...state,[prop]: value})
      }  

      const HandleCustomerSave =_=>{
        dispatch(toggleLoadScreen(true))

        if(!props.customerDetails){
            CustomerAccountService.CreateCustomerAccount(state)
            .then(_=>{
                dispatch(toggleModal(false));
    
                setTimeout(() => {
                    dispatch(setActiveCustomer(null));                
                    toggleLoadScreen(false)
                    dispatch(showSnackbar({show: true, message: "Customer created successfully"}));
                }, 1000);
            })
        }
        else{
            CustomerAccountService.UpdateCustomerAccount(state)
            .then(_=>{
                dispatch(toggleModal(false));
    
                setTimeout(() => {
                    dispatch(setActiveCustomer(null));                
                    toggleLoadScreen(false)
                    dispatch(showSnackbar({show: true, message: "Customer updated successfully"}));
                }, 1000);
            })
        }
      }

      const CheckDisabledSaveButton=_=>{
        if(props.customerDetails)
        return false;

        if(firstName && lastName && email)
        return false;

        return true;
      }

return(
    <div className="accountDetailBox">       
        <TextField  
        variant="outlined" 
        label="First name" 
        value={firstName|| ""}
         style={{marginBottom: '1.5vh'}} 
         onChange={e=> HandleChange('firstName', e.target.value)}/>
        <TextField  
        variant="outlined" 
        label="Last name" 
        value={lastName || ""} 
        style={{marginBottom: '1.5vh'}} 
        onChange={e=> HandleChange('lastName', e.target.value)}/>
        <TextField  
        variant="outlined"
         label="Email address" 
         value={email|| ""} 
         style={{marginBottom: '1.5vh'}} 
         onChange={e=> HandleChange('email', e.target.value)}/>
        <TextField  
        variant="outlined" 
        label="Account number" 
        value={accountNumber||""} 
        style={{marginBottom: '1.5vh'}} 
        disabled={true}/>
        <TextField  
        variant="outlined" 
        label="IBAN" 
        value={iban||""} 
        style={{marginBottom: '1.5vh'}} 
        disabled={true}/>
        <TextField  
        variant="outlined" 
        label="Balance" 
        value={balance || ""} 
        type="number"
        onChange={e=> HandleChange('balance', e.target.value)}
        style={{marginBottom: '1.5vh'}} 
        disabled={true}/>
        <Button variant="contained" style={{alignSelf: 'center'}} onClick={_=> HandleCustomerSave()} disabled={CheckDisabledSaveButton()}>Save</Button>
    </div>
)
}