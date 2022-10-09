import React from "react";
import { Backdrop, CircularProgress } from "@mui/material";
import { useSelector } from "react-redux";

export default _ => {
    const show = useSelector(s => s.navigation.showBackdrop)

    return <Backdrop open={show}>
        <CircularProgress style={{ 'color': 'blue' }} />
    </Backdrop>
}