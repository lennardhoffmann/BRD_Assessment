import { Button, TextField } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { setActiveCustomer } from "../../state/stateFeatures/customerSlice";

export default props =>{
    const dispatch = useDispatch();
    const [state,setState] = useState({
        firstName: null,
        lastName: null,
        email: null,
        iban: null,
        balance: null
        });

        const {firstName,lastName, email,iban, balance} = state;

        useEffect(_=>{
            if(props.customerDetails && !firstName && !lastName && !email && !iban && !balance){
    setState({
        firstName: props.customerDetails.firstName,
        lastName:props.customerDetails.lastName,
        email:props.customerDetails.email,
        iban:props.customerDetails.iban,
        balance:props.customerDetails.balance
    })
            }
        })

      const HandleChange= (prop,value)=>{
        setState({...state,[prop]: value})
      }  

return(
    <div style={{display:'flex', flexDirection: 'column',width: '60%',height:'100%', padding: '1vh',  marginTop: '2vh'}}>
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
        value={iban||""} 
        style={{marginBottom: '1.5vh'}} 
        disabled={true}/>
        <TextField  
        variant="outlined" 
        label="Balance" 
        value={balance || 0} 
        style={{marginBottom: '1.5vh'}} 
        disabled={true}/>
        <Button variant="contained" style={{alignSelf: 'center'}} onClick={_=> dispatch(setActiveCustomer(state))}>Save</Button>
    </div>
)
}