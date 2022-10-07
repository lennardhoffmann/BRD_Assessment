import { createSlice } from '@reduxjs/toolkit'

export const customerSlice = createSlice({
    name: 'customers',
    initialState: {
        customers: null,
        activeCustomer: null,
        showModal: false
    },
    reducers: {
        setActiveCustomer: (state, action) => {
            state.activeCustomer = action.payload
        },
        setCustomerList: (state, action) => {
            state.customers = action.payload
        },
        toggleModal:(state, action)=>{
state.showModal = action.payload
        },
        clearState: (state) => {
            state.activeCustomer = null;
            state.customers = null;
            state.showModal= false;
        }
    }
})

// Action creators are generated for each case reducer function
export const { setActiveCustomer, setCustomerList,toggleModal, clearState} = customerSlice.actions

export default customerSlice.reducer