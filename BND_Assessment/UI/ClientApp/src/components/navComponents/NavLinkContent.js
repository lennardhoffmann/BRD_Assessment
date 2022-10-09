import React from "react";

import './_style.navLinkContent.scss';

export default props =>{
    return(
        <div className="navContentContainer">
            <div className="navIconBox">{props.icon}</div>
            |
            <div className="navTextBox">
            {props.label}
            </div>
        </div>
    )
}