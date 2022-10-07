import { configureStore } from '@reduxjs/toolkit';
import { customerReducer } from './stateFeatures';

export default configureStore({
    reducer: {
        customers: customerReducer
    }
})