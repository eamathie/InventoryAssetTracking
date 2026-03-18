import { useEffect, useState } from "react"
import { assetsAllRequest } from "../../tools/AssetsHelper"


type Asset = {
    name: string
    categoryId: number
    status: string
    purchaseDate: Date
    userId: string
    notes: string
}

const Assets = () => {
    const [assetNames, setAssetNames] = useState<Asset[]>([])

    useEffect(() => {
        handleRequest()
    }, [])
    
    const handleRequest = async () => {
        const response = await assetsAllRequest()
        console.log(response)
    }

    return (
        <div>
            
        </div>
    )
}

export default Assets