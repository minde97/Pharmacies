import Button from '@mui/material/Button'
import { styled } from '@mui/material/styles'
import Dialog from '@mui/material/Dialog'
import DialogTitle from '@mui/material/DialogTitle'
import DialogContent from '@mui/material/DialogContent'
import DialogActions from '@mui/material/DialogActions'
import Typography from '@mui/material/Typography'
import { usePharmaciesApi } from '../../../hooks/usePharmaciesApi.ts'
import { SetStateAction, useState } from 'react'
import { CircularProgress } from '@mui/material'

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
    '& .MuiDialogContent-root': {
        padding: theme.spacing(2),
    },
    '& .MuiDialogActions-root': {
        padding: theme.spacing(1),
    },
}))

interface UpdatePostCodesDialogProps {
    open: boolean
    onClose: () => void
    onUpdateClose: () => void
}

export function UpdatePostCodesDialog(
    props: UpdatePostCodesDialogProps
) {
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(null)
    const [result, setResult] = useState<boolean>(false)
    const pharmaciesApi = usePharmaciesApi()

    const handleUpdate = async () => {
        setLoading(true)
        try {
            await pharmaciesApi.updatePostCodes()
            setResult(true)
        } catch (error) {
            setError(error as SetStateAction<any>)
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
                {!loading && !result ? <>Update post codes</> : null}
                {loading ? <>Post codes are being updated</> : null}
                {result ? <>Post Codes Updated</> : null}
                {error != null ? (
                    <>Failed to update post codes: {(error as Error).message}</>
                ) : null}
            </DialogTitle>
            {!loading && !result ? (
                <DialogContent dividers>
                    <Typography gutterBottom>
                        By clicking update you will update every pharmacy post
                        code. Are you sure you want to update it?
                    </Typography>
                </DialogContent>
            ) : null}
            {loading ? (
                <DialogContent
                    style={{ justifyContent: 'center', display: 'flex' }}
                >
                    <CircularProgress></CircularProgress>
                </DialogContent>
            ) : null}
            <DialogActions>
                {!loading && !result ? (
                    <>
                        <Button autoFocus onClick={handleUpdate}>
                            Update
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
                            props.onUpdateClose()
                        }}
                    >
                        Close
                    </Button>
                ) : null}
            </DialogActions>
        </BootstrapDialog>
    )
}
