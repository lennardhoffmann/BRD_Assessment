import { DataGrid } from "@mui/x-data-grid";
import React, { useEffect, useState } from "react";
import { CustomerAccountService } from "../../services";

import './_style.transactionDetailComponent.scss'

export default props=>{

    const[state,setState] = useState({
        transactions: null
    });

    const {transactions} = state;

    useEffect(_=>{
       if(props.customerDetails){
        if(!transactions){
            CustomerAccountService.GetCustomerAccountTransactions(props.customerDetails.id)
            .then(res=>{
               if(res && res.length > 0)
               setState({...state, transactions: res})
            })
        }
       }
    });

    const columns = [
        {
            field: "transactionDate",
            flex: 1,
            renderHeader: () =>(
                <label style={{fontWeight: 'bold'}}>Transaction date</label>
            ),
            renderCell: (cellValues)=>(
                <label>{formatDate(cellValues.row.transactionDate)}</label>
            )
        },
        {
            field: "description",
            flex: 1,
            renderHeader: () =>(
                <label style={{fontWeight: 'bold'}}>Description</label>
            ),
            renderCell: (cellValues)=>(
                <label>{cellValues.row.description}</label>
            )
        },
        {
            field: "amount",
            flex: 1,
            renderHeader: () =>(
                <label style={{fontWeight: 'bold'}}>Transaction amount</label>
            ),
            renderCell: (cellValues)=>(
                <label>{cellValues.row.amount}</label>
            )
        }
    ];

    const formatDate = date =>{
        

        return date.substring(0, 10)
    }

    return(
        <div className="txBox">
{transactions && transactions.length> 0 ? 
<DataGrid
columns={columns}
checkboxSelection={false}
rows={transactions}
rowsPerPageOptions={[5, 10, 20]}
/>
: 
<label>No transactions recorder yet for this account</label>}
        </div>
    )
}