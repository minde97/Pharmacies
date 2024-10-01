import Table from '@mui/material/Table'
import TableBody from '@mui/material/TableBody'
import TableCell from '@mui/material/TableCell'
import TableContainer from '@mui/material/TableContainer'
import TableHead from '@mui/material/TableHead'
import TableRow from '@mui/material/TableRow'
import Paper from '@mui/material/Paper'
import { PharmacyListItem } from '../../../apiClients/PharmacyApi'
import { SetStateAction, useEffect, useState } from 'react'
import { usePharmaciesApi } from '../../../hooks/usePharmaciesApi.ts'

export function PharmaciesTable() {
    const pharmaciesApi = usePharmaciesApi()
    const [pharmacies, setPharmacies] = useState<PharmacyListItem[]>([])
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(null)

    useEffect(() => {
        const fetchData = async () => {
            try {
                setLoading(true)
                const getPharmaciesResponse =
                    await pharmaciesApi.getPharmacies()
                setPharmacies(getPharmaciesResponse.data.pharmacies)
            } catch (error) {
                setError(error as SetStateAction<any>)
            } finally {
                setLoading(false)
            }
        }

        fetchData()
    }, [])

    if (loading) return <div>Loading...</div>
    if (error) {
        return <div>Error: {(error as Error).message}</div>
    }

    return (
        <TableContainer component={Paper}>
            <Table aria-label="pharmacies-table">
                <TableHead>
                    <TableRow>
                        <TableCell>Name</TableCell>
                        <TableCell>Address</TableCell>
                        <TableCell>Post Code</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {pharmacies.map((pharmacy) => (
                        <TableRow key={pharmacy.id}>
                            <TableCell>{pharmacy.name}</TableCell>
                            <TableCell>{pharmacy.address}</TableCell>
                            <TableCell>{pharmacy.postCode}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}
