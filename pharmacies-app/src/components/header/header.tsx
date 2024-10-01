import {
    AppBar,
    Box,
    Button,
    IconButton,
    Toolbar,
    Typography,
} from '@mui/material'

interface HeaderProps {
    onUpdatePostCodesClick: () => void
    onImportClick: () => void
}
export function Header(props: HeaderProps) {
    return (
        <Box>
            <AppBar position="static">
                <Toolbar>
                    <IconButton
                        size="large"
                        edge="start"
                        color="inherit"
                        aria-label="menu"
                        sx={{ mr: 2 }}
                    ></IconButton>
                    <Typography
                        variant="h5"
                        component="div"
                        sx={{ flexGrow: 1 }}
                    >
                        Pharmacies
                    </Typography>
                    <Button
                        color="inherit"
                        onClick={() => {
                            props.onImportClick()
                        }}
                    >
                        Import
                    </Button>
                    <Button
                        color="inherit"
                        onClick={() => {
                            props.onUpdatePostCodesClick()
                        }}
                    >
                        Update Post Codes
                    </Button>
                </Toolbar>
            </AppBar>
        </Box>
    )
}
