import { createTheme } from '@mui/material'
import { green } from '@mui/material/colors'

const theme = createTheme({
    palette: {
        primary: {
            main: green[500],
            contrastText: '#fff',
        },
    },
})

export default theme
