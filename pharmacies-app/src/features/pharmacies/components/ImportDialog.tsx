import { styled } from '@mui/material/styles'
import Dialog from '@mui/material/Dialog'
import DialogTitle from '@mui/material/DialogTitle'
import { usePharmaciesApi } from '../../../hooks/usePharmaciesApi.ts'
import { SetStateAction, useState } from 'react'
import Button from '@mui/material/Button'
import DialogActions from '@mui/material/DialogActions'
import DialogContent from '@mui/material/DialogContent'
import { Box, CircularProgress, DialogContentText, Input } from '@mui/material'
import { ImportPharmaciesResponse } from '../../../apiClients/PharmacyApi'

interface ImportDialogProps {
    open: boolean
    onClose: () => void
    onImportClose: () => void
    fileAdded: boolean
    onFileAdded: () => void
}

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
    '& .MuiDialogContent-root': {
        padding: theme.spacing(2),
    },
    '& .MuiDialogActions-root': {
        padding: theme.spacing(1),
    },
}))

export function ImportDialog(props: ImportDialogProps) {
    const pharmaciesApi = usePharmaciesApi()
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(null)
    const [file, setFile] = useState<File | null>(null)
    const [result, setResult] = useState<ImportPharmaciesResponse | null>(null)

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files) {
            setFile(event.target.files[0])
        }

        props.onFileAdded()
    }

    const handleImport = async (event: React.FormEvent) => {
        event.preventDefault()

        if (!file) {
            alert('Please select a file')
            return
        }

        if (file.type != 'application/json') {
            alert('Only json file types are supported')
            return
        }

        try {
            setLoading(true)
            const response = await pharmaciesApi.importPharmacies(file)

            if (response.status === 200) {
                setResult(response.data)
            }

            console.log(response.data)
            setError(null)
        } catch (error) {
            setError(error as SetStateAction<any>)
            console.log(error)
        } finally {
            setLoading(false)
        }
    }

    return (
        <BootstrapDialog
            onClose={() => {
                props.onClose
            }}
            aria-labelledby="customized-dialog-title"
            open={props.open}
        >
            <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title">
                {!loading && !result ? <>Import Pharmacies</> : null}
                {loading ? <>Pharmacies are being uploaded</> : null}
                {result ? <>Pharmacies uploaded {result.pharmaciesUploaded}</> : null}
                {error != null ? (
                    <>Failed to upload pharmacies: {(error as Error).message}</>
                ) : null}
            </DialogTitle>
            <DialogContent>
                {!loading && !result ? (
                    <>
                        <DialogContentText sx={{ pb: 2 }}>
                            Upload a .json file for pharmacy upload
                        </DialogContentText>
                        <Box
                            style={{
                                justifyContent: 'center',
                                display: 'flex',
                            }}
                        >
                            <Button
                                component="label"
                                role={undefined}
                                variant="contained"
                                tabIndex={-1}
                            >
                                {!props.fileAdded ? (
                                    <>Upload file</>
                                ) : (
                                    <>File uploaded</>
                                )}
                                <Input
                                    sx={{ display: 'none' }}
                                    type="file"
                                    onChange={handleFileChange}
                                />
                            </Button>
                        </Box>
                    </>
                ) : null}
                {loading ? (
                    <DialogContent
                        style={{ justifyContent: 'center', display: 'flex' }}
                    >
                        <CircularProgress></CircularProgress>
                    </DialogContent>
                ) : null}
            </DialogContent>

            <DialogActions>
                {!loading && !result ? (
                    <>
                        <Button type="submit" onClick={handleImport}>
                            Import
                        </Button>
                        <Button
                            autoFocus
                            onClick={() => {
                                props.onClose()
                            }}
                        >
                            Close
                        </Button>
                    </>
                ) : error == null ? (
                    <></>
                ) : (
                    <Button
                        autoFocus
                        onClick={() => {
                            props.onClose()
                        }}
                    >
                        Close
                    </Button>
                )}
                {result ? (
                    <Button
                        autoFocus
                        onClick={() => {
                            props.onImportClose()
                        }}
                    >
                        Close
                    </Button>
                ) : null}
            </DialogActions>
        </BootstrapDialog>
    )
}
