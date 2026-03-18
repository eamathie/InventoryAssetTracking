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
        <div className="min-h-screen bg-gradient-to-r from-blue-400 to-purple-500 justify-center">
            <div className="flex flex-col gap-4 p-5">
                <div className="text-3xl font-bold">
                    Assets
                </div>
                <div className="grid grid-cols-5 gap-4">
                    {assets.map((a, index) => <Asset key={index}
                    name={a.name} 
                    categoryId={a.categoryId} 
                    status={a.status} 
                    purchaseDate={a.purchaseDate}
                    userId={a.userId}
                    notes={a.notes}
                    /> )}
                </div>
            </div>
        </div>
    )
}

export default Assets