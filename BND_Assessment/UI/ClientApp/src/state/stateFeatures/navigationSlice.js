import { createSlice } from '@reduxjs/toolkit'

export const navigationSlice = createSlice({
    name: 'navigation',
    initialState: {
        showBackdrop: false,
        showSnackbar: {
            show: false,
            message: ''
        },
    },
    reducers: {
        toggleLoadScreen: (state, action) => {
            state.showBackdrop = action.payload
        },
        showSnackbar: (state, action)=>{
            state.showSnackbar = action.payload
        },
        updateBreadcrumbs: (state, action)=>{
            state.path = action.payload
        }
    }
})

// Action creators are generated for each case reducer function
export const { toggleLoadScreen,showSnackbar, updateBreadcrumbs } = navigationSlice.actions

export default navigationSlice.reducer