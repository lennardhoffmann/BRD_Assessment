import React, { useEffect, useState } from "react";
import { Button, FormControl, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";

import './_style.transferComponent.scss';
import { TransferService } from "../../services";
import { showSnackbar, toggleLoadScreen } from "../../state/stateFeatures/navigationSlice";

export default _ => {
    const customerList = useSelector(s => s.customers).customers;
    const dispatch = useDispatch();

    const [state, setState] = useState({
        sourceCustomerList: null,
        targetCustomerList: null,
        sourceCustomer: null,
        targetCustomer: null,
        amount: null,
        amountError: false,
        maxAmount: null
    });

    const { sourceCustomer, targetCustomer, sourceCustomerList, targetCustomerList, amount, amountError, maxAmount } = state;

    useEffect(_ => {
        if (customerList) {
            if (!sourceCustomerList)
                setState({ ...state, sourceCustomerList: customerList })
        }
    })

    const handleSourceSelect = sourceId => {
        var filteredArray = [...sourceCustomerList];
        var index = filteredArray.findIndex(c => c.id == sourceId);
        let max = filteredArray[index].balance

        filteredArray.splice(index, 1);

        setState({ ...state, sourceCustomer: sourceId, targetCustomerList: filteredArray, maxAmount: max });
    }

    const handleTargetSelect = targetId => {
        setState({ ...state, targetCustomer: targetId })
    }

    const handleAmountChange = amt => {
        if (amt && (amt < 1 || amt > maxAmount)) {
            setState({ ...state, amountError: true, amount: null });
            return;
        }

        setState({ ...state, amount: amt, amountError: false })
    }

    const handleTransfer = _ => {
        dispatch(toggleLoadScreen(true));

        var transferObj = {
            "sourceCustomerAccountId": sourceCustomer,
            "destinationCustomerAccountId": targetCustomer,
            "amount": amount
        };

        TransferService.MakeAccountTransfer(transferObj)
            .then(_ => {
                setTimeout(() => {
                    dispatch(toggleLoadScreen(false));
                    dispatch(showSnackbar({ show: true, message: `An amount of ${amount} has successfully been transferred between the accounts` }));
                }, 1000);
            });

        setState({
            sourceCustomerList: null,
            targetCustomerList: null,
            sourceCustomer: null,
            targetCustomer: null,
            amount: null,
            amountError: false,
            maxAmount: null
        });
    }

    return (
        <div className="trfComponentBox">
            <label style={{ fontSize: '2vh', marginBottom: '2vh', fontWeight: 'bold' }}>Transfer amount between customer accounts</label>
            <FormControl fullWidth>
                <InputLabel id="select-customer-label">Source customer account</InputLabel>
                <Select
                    value={sourceCustomer || ""}
                    labelId="select-customer-label"
                    label="Source customer account"
                    onChange={e => handleSourceSelect(e.target.value)}
                >
                    {sourceCustomerList && sourceCustomerList.length > 0 && sourceCustomerList.map((customer, i) => {
                        return <MenuItem key={`key-source-${i}-${customer.accountNumber}`} value={customer.id}>{`${customer.firstName} ${customer.lastName} | ${customer.iban}`}</MenuItem>
                    })}
                </Select>
            </FormControl>
            <div style={{ marginBottom: '2vh' }} />
            <FormControl fullWidth>
                <InputLabel id="select-customer-label">Target customer account</InputLabel>
                <Select
                    value={targetCustomer || ""}
                    labelId="select-customer-label"
                    label="Target customer account"
                    onChange={e => handleTargetSelect(e.target.value)}
                    disabled={!sourceCustomer && true}
                >
                    {targetCustomerList && targetCustomerList.length > 0 && targetCustomerList.map((customer, i) => {
                        return <MenuItem key={`key-target-${i}-${customer.accountNumber}`} value={customer.id}>{`${customer.firstName} ${customer.lastName} | ${customer.iban}`}</MenuItem>
                    })}
                </Select>
            </FormControl>
            <TextField
                disabled={!sourceCustomer && !targetCustomer && true}
                error={amountError}
                helperText={amountError ? `Please specify an amount greater than 0 and less than ${maxAmount}` : ''}
                variant="outlined"
                label="Amount"
                value={amount || ""}
                type="number"
                onChange={e => handleAmountChange(e.target.value)}
                style={{ marginTop: '2vh', width: '40%' }} />
            <label style={{ fontStyle: 'italic' }}>{maxAmount && `Please not the maximum aount available for transfer is limited to ${maxAmount}. This is the balance of the source account`}</label>
            <Button
                variant="contained"
                disabled={(!sourceCustomer || !sourceCustomer || !amount) && true}
                onClick={_ => handleTransfer()}
                style={{ alignSelf: 'center', marginTop: '3vh', width: '30%' }}
            >
                Transer amount</Button>
        </div>
    )
}