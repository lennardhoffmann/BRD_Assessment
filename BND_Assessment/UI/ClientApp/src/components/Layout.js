import React from 'react';
import {BrowserRouter as Router, Switch,Routes,Route } from 'react-router-dom';
import { appRoutes } from '../utils';
import BackdropComponent from './BackdropComponent';
import CustomersComponent from './customerComponents/CustomersComponent';
import DepositComponent from './depositComponents/DepositComponent';
import { NavMenu } from './navComponents';
import SnackbarComponent from './SnackbarComponent';
import { TransferComponent } from './transferComponents';

export default _ => {
    return (
        <div style={{width: '100vw', height: '100vh'}}>
            <div style={{display: 'flex', flexDirection: 'row', width: '100%',  height: '100%'}}>
              <NavMenu/>
              <div style={{display:'flex',flexDirection:'column', width:'100%', height:'100%', padding: '1vh', justifyContent:'center', alignItems:'center'}}>
               <Routes>
                  <Route path={appRoutes.customers} element={<CustomersComponent/>} exact/>
                  <Route path={appRoutes.deposits} element={<DepositComponent/>} exact/>
                  <Route path={appRoutes.transfers} element={<TransferComponent/>} exact/>
               </Routes>
               <BackdropComponent/>
               <SnackbarComponent/>
              </div>
            </div>
      </div>
    );
}
