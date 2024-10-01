import { ThemeProvider } from '@mui/material'
import theme from './theme.ts'
import { useState } from 'react'
import { ImportDialog, PharmaciesTable, UpdatePostCodesDialog } from './features'
import { Header } from './components'

function App() {
    const [openUpdateDialog, setOpenUpdateDialog] = useState(false)
    const [openImportDialog, setImportDialog] = useState(false)
    const [fileAdded, setFileAdded] = useState(false)

    return (
        <ThemeProvider theme={theme}>
            <Header
                onUpdatePostCodesClick={() => setOpenUpdateDialog(true)}
                onImportClick={() => setImportDialog(true)}
            ></Header>
            <ImportDialog
                open={openImportDialog}
                onClose={() => {
                    setImportDialog(false)
                    setFileAdded(false)
                    window.location.reload()
                }}
                onImportClose={() => {
                    setOpenUpdateDialog(false)
                    setFileAdded(false)
                    window.location.reload()
                }}
                fileAdded={fileAdded}
                onFileAdded={() => setFileAdded(true)}
            />
            <UpdatePostCodesDialog
                open={openUpdateDialog}
                onClose={() => setOpenUpdateDialog(false)}
                onUpdateClose={() => {
                    setOpenUpdateDialog(false)
                    window.location.reload()
                }}
            />
            <PharmaciesTable></PharmaciesTable>
        </ThemeProvider>
    )
}

export default App
