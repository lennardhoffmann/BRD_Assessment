import { configureStore } from '@reduxjs/toolkit';
import { customerReducer, navigationReducer } from './stateFeatures';

export default configureStore({
    reducer: {
        customers: customerReducer,
        navigation: navigationReducer
    }
})