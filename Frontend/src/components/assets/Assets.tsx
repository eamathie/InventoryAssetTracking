import { useEffect, useState } from "react"
import { assetsAllRequest } from "../../tools/AssetsHelper"
import Asset from "./Asset"
import AssetDetails from "./AssetDetails"
import { useNavigate } from "react-router"

export type AssetResponse = {
    id: number
    name: string
    categoryId: number
    status: string
    purchaseDate: Date
    userId: string
    qrCodePath: string
    notes: string
}

const Assets = () => {
    const [assets, setAssets] = useState<AssetResponse[]>([])
    const [open, setOpen] = useState(false)
    const [selectedIndex, setSelectedIndex] = useState<number>(0)
    const navigate = useNavigate()

    useEffect(() => {
        handleRequest()
    }, [])
    
    const handleRequest = async () => {
        try {
            const response = await assetsAllRequest()
            setAssets(response)
        } catch {
            navigate("/auth")
        }
    }

    const handleOpen = (index: number) => {
        setSelectedIndex(index)
        setOpen(true)
    }   
    const handleClose = () => setOpen(false)

    return (
        <div className="flex flex-col flex-1 max-h-[494px] mt-[64px] gap-4 p-5">
            <div className="text-3xl font-bold">
                Assets
            </div>
            <div className="grid grid-cols-5 gap-4 max-h-full overflow-y-scroll rounded-lg">
                {assets.map((a, index) => <Asset key={index}
                name={a.name} 
                categoryId={a.categoryId} 
                status={a.status} 
                purchaseDate={a.purchaseDate}
                userId={a.userId}
                notes={a.notes}
                onClick={() => handleOpen(index)}
                />)
            }
            </div>
            {selectedIndex !== null && <AssetDetails assetData={assets[selectedIndex]} open={open} onClose={handleClose}/>}
        </div>
    )
}

export default Assets