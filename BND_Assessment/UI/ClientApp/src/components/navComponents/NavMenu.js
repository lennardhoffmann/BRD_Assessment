import React from "react";
import { NavLink } from "react-router-dom";
import PeopleIcon from '@mui/icons-material/People';
import MonetizationOnIcon from '@mui/icons-material/MonetizationOn';
import CurrencyExchangeIcon from '@mui/icons-material/CurrencyExchange';
import NavLinkContent from "./NavLinkContent";

import './_style.navMenu.scss'

export default _=>{
    return(
        <div className="navMenuContainer">
           <NavLink to="/customers" className="navLink">
                <NavLinkContent label="Customers" icon={<PeopleIcon fontSize="large"/>}/>
            </NavLink>
           <NavLink to="/deposits" className="navLink">
            <NavLinkContent icon={<MonetizationOnIcon fontSize="large"/>} label="Deposits"/>
            </NavLink>
           <NavLink to="/transfers" className="navLink">
            <NavLinkContent icon={<CurrencyExchangeIcon fontSize="large"/>} label="Transfers"/>
                        </NavLink>
        </div>
    )
}