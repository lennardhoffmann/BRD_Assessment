import React, {  useState } from "react";
import { Tabs,Tab } from "@mui/material";
import { useSelector } from "react-redux";
import AccountDetailComponent from "./AccountDetailComponent";
import TransactionDetailComponent from "./TransactionDetailComponent";

import './_style.customerDetailComponent.scss';

export default props =>{
    const activeCustomer = useSelector(s=> s.customers).activeCustomer;
    const [state,setState] = useState({
        tab:0
    });

    const {tab} = state;

    const handleChange = (event, newValue) => {
        setState({
            ...state,
            tab: newValue           
        });
    };

    const GetCurrentView=_=>{
        switch(tab){
            case 0:
                return <AccountDetailComponent customerDetails={activeCustomer}/>;
                case 1:
                    return <TransactionDetailComponent customerDetails={activeCustomer}/>
                default: break;
        }
    }

    return(
        <div className="customerDetailBox">
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