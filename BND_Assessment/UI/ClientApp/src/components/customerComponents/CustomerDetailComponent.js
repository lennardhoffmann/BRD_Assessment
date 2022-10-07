import React, { useEffect, useState } from "react";
import { Tabs,Tab } from "@mui/material";
import { useSelector } from "react-redux";
import AccountDetailComponent from "./AccountDetailComponent";

export default props =>{
    const activeCustomer = useSelector(s=> s.customers).activeCustomer;
    const [state,setState] = useState({
        tab:0
    });

    const {tab} = state;

    useEffect(_=>{
        console.log('state', activeCustomer)
    })

    const handleChange = (event, newValue) => {
        setState({
            ...state,
            tab: newValue           
        });
    };

    const GetCurrentView=_=>{
        switch(tab){
            case 0:
                return <AccountDetailComponent customerDetails={activeCustomer}/>
                default: break;
        }
    }

    return(
        <div style={{display: 'flex', flexDirection: 'column', width: '100%',height: '100%', padding: '1vh'}}>
            <label>Customer Account Details</label>

            <div style={{width:'100%'}}>
                <Tabs value={tab} aria-label="basic tabs example" onChange={handleChange}>
                    <Tab label="Account Details"/>
                    <Tab label="Transaction History"/>
                </Tabs>
                {GetCurrentView()}
            </div>
        </div>
    )
}