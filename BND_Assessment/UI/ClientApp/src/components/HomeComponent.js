import React from "react";

export default _ => {
    return (
        <div style={{ display: 'flex', flexDirection: 'column', alignSelf: 'center' }}>
            <span>Welcome to my assessment solution. Feel free to navigate the left menu</span>
            <label style={{ marginTop: '2vh' }}>In short:</label>
            <p>The <strong>Customers</strong> link is used to create accounts and view accounts of customers</p>
            <p>The <strong>Deposits</strong> link is used to deposit amounts into the account of a customer</p>
            <p>The <strong>Transfers</strong> link is used to transfer amounts between customeraccounts</p>
        </div>
    )
}