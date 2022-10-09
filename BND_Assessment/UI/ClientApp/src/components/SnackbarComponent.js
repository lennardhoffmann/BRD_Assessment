import React, { useState } from 'react'
import Snackbar from '@mui/material/Snackbar';
import { useSelector } from "react-redux";
import { store } from '../state';
import { showSnackbar } from '../state/stateFeatures/navigationSlice';

export default _ =>{
    const state = useSelector(s => s.navigation.showSnackbar);
    const [anchor, setAnchor] = useState({
        vertical: 'top',
        horizontal: 'center'
    })

    if(state.show){
        setTimeout(() => {
            store.dispatch(showSnackbar({'show': false, 'message': ""}))
        }, 3000);
    }

    const {vertical, horizontal} = anchor;
    
    return <Snackbar open={state.show} message={state.message} anchorOrigin={{vertical, horizontal}}/>
}