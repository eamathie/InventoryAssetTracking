import { useEffect, useState } from "react"
import { assetsAllRequest } from "../../tools/AssetsHelper"
import Asset from "./Asset"


type Asset = {
    name: string
    categoryId: number
    status: string
    purchaseDate: Date
    userId: string
    notes: string
}

const Assets = () => {
    const [assets, setAssets] = useState<Asset[]>([])

    useEffect(() => {
        handleRequest()
    }, [])
    
    const handleRequest = async () => {
        const response = await assetsAllRequest()
        setAssets(response)
    }

    return (
        <div>
            {assets.map((a, index) => <Asset key={index}
            name={a.name} 
            categoryId={a.categoryId} 
            status={a.status} 
            purchaseDate={a.purchaseDate}
            userId={a.userId}
            notes={a.notes}
             /> )}
        </div>
    )
}

export default Assets