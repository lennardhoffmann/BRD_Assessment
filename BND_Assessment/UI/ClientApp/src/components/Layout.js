import React from 'react';
import {BrowserRouter as Router, Switch,Routes,Route } from 'react-router-dom';
import { appRoutes } from '../utils';
import CustomersComponent from './customerComponents/CustomersComponent';
import { NavMenu } from './navComponents';

export default _ => {
    return (
        <div style={{width: '100vw', height: '100vh'}}>
            <div style={{display: 'flex', flexDirection: 'row', width: '100%',  height: '100%'}}>
              <NavMenu/>
              <div style={{display:'flex',flexDirection:'column', width:'100%', height:'100%', padding: '1vh'}}>
               <Routes>
                  <Route path={appRoutes.customers} element={<CustomersComponent/>} exact/>
               </Routes>
              </div>
            </div>
      </div>
    );
}
