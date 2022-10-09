import { apiPaths } from "../config"
import { store } from "../state"
import { setCustomerList } from "../state/stateFeatures/customerSlice"

class _DepositService{
MakeCustomerDeposit = depositDetails =>{
    return new Promise((resolve, reject) => {
        fetch(
            apiPaths.makeDeposit,
            {
                method: "PUT",
                body: JSON.stringify(depositDetails),
                headers: {
                    'Content-Type': 'application/json'
                },
            }
        )
            .then(res => res.json())
            .then(result => {
                console.log(result)
                if (result) {
                    var customers = [...store.getState().customers.customers]
                    var index = customers.findIndex(c=> c.id == result.id);
                    
                    customers[index] = result;

                    store.dispatch(setCustomerList(customers));
                }

                resolve();
            })
            .catch(err => reject(err))
    })
}
}

export const DepositService = new _DepositService()