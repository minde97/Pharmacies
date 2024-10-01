import { Configuration, PharmaciesApi } from '../apiClients/PharmacyApi'

export const usePharmaciesApi = () => {
    return new PharmaciesApi(
        new Configuration({
            basePath: 'http://localhost:5088',
        })
    )
}
