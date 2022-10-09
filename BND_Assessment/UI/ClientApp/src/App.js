import React, { useEffect } from 'react';
import { LayoutComponent } from './components';
import { CustomerAccountService } from './services';

export default _ => {
  useEffect(_=>{
    CustomerAccountService.GetCustomerAccounts();
  })
    return (
      <LayoutComponent/>
    );
}
