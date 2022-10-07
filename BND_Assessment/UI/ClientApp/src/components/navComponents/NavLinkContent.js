import React from "react";

import './_style.navLinkContent.scss';

export default props =>{
    return(
        <div className="navContentContainer">
            <div style={{height:'100%', width: '20%', display: 'flex', justifyContent:'center', alignItems: 'center'}}>{props.icon}</div>
            |
            <div style={{height:'100%', width: '70%', display: 'flex', justifyContent:'center', alignItems: 'center'}}>
            {props.label}
            </div>
        </div>
    )
}