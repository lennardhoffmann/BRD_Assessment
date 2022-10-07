import React from "react";
import { NavLink } from "react-router-dom";
import PeopleIcon from '@mui/icons-material/People';
import PaymentsIcon from '@mui/icons-material/Payments';
import MonetizationOnIcon from '@mui/icons-material/MonetizationOn';
import NavLinkContent from "./NavLinkContent";

import './_style.navMenu.scss'
import { Link } from "react-router-dom";

export default _=>{
    return(
        <div className="navMenuContainer">
           <NavLink to="/customers" className="navLink">
                <NavLinkContent label="Customers" icon={<PeopleIcon fontSize="large"/>}/>
            </NavLink>
           <NavLink to="/transfers" className="navLink">
            <NavLinkContent icon={<PaymentsIcon fontSize="large"/>} label="Transfers"/>
            </NavLink>
           <NavLink to="/" className="navLink">
            <NavLinkContent icon={<MonetizationOnIcon fontSize="large"/>} label="Service Charges"/>
                        </NavLink>
        </div>
    )
}