import { apiPaths } from "../config"
import { CustomerAccountService } from "./CustomerAccountService"

class _TransferService{
MakeAccountTransfer = transferDetails =>{
    return new Promise((resolve, reject) => {
        fetch(
            apiPaths.makeTransfer,
            {
                method: "PUT",
                body: JSON.stringify(transferDetails),
                headers: {
                    'Content-Type': 'application/json'
                },
            }
        )
            .then(res => res.json())
            .then(result => {                
                if (result) {
                   CustomerAccountService.GetCustomerAccounts();
                }

                resolve();
            })
            .catch(err => reject(err))
    })
}
}

export const TransferService = new _TransferService()