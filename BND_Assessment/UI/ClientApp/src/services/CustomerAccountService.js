import { apiPaths } from "../config";
import { setCustomerList } from "../state/stateFeatures/customerSlice";
import store from "../state/store";

class _CustomerAccountService{
GetCustomerAccounts=_=>{
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
}

export const CustomerAccountService = new _CustomerAccountService();