import { apiPaths } from "../config";
import {  setCustomerList } from "../state/stateFeatures/customerSlice";
import store from "../state/store";

class _CustomerAccountService {
    GetCustomerAccounts = _ => {
        return new Promise((resolve, reject) => {
            fetch(
                apiPaths.getCustomers,
                {
                    method: "GET"
                }
            )
                .then(res => res.json())
                .then(accounts => {
                    if (accounts && accounts.length > 0)
                        store.dispatch(setCustomerList(accounts));

                    resolve();
                })
                .catch(err => reject(err))
        })
    }

    CreateCustomerAccount = customerData => {
        return new Promise((resolve, reject) => {
            fetch(
                apiPaths.createCustomer,
                {
                    method: "POST",
                    body: JSON.stringify(customerData),
                    headers: {
                        'Content-Type': 'application/json'
                    },
                }
            )
                .then(res => res.json())
                .then(result => {
                    if (result) {
                        var customers = [...store.getState().customers.customers]
                        customers.push(result)

                        store.dispatch(setCustomerList(customers));
                    }

                    resolve();
                })
                .catch(err => reject(err))
        })
    }

    UpdateCustomerAccount = customerData => {
        return new Promise((resolve, reject) => {
            fetch(
                apiPaths.updateCustomer.replace('{Param}', customerData.id),
                {
                    method: "PUT",
                    body: JSON.stringify(customerData),
                    headers: {
                        'Content-Type': 'application/json'
                    },
                }
            )
                .then(res => res.json())
                .then(result => {
                    if (result) {
                        var customers = [...store.getState().customers.customers]
                        var index = customers.findIndex(c => c.id == result.id);

                        customers[index] = result;

                        store.dispatch(setCustomerList(customers));
                    }

                    resolve();
                })
                .catch(err => reject(err))
        })
    }

    GetCustomerAccountTransactions = id => {
        return new Promise((resolve, reject) => {
            fetch(
                apiPaths.getCustomerAccountTransactions.replace('{Param}', id),
                {
                    method: "GET"
                }
            )
                .then(res => res.json())
                .then(transactions => {
                    resolve(transactions);
                })
                .catch(err => reject(err))
        })
    }
}

export const CustomerAccountService = new _CustomerAccountService();