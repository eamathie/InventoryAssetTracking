import { useEffect, useState } from "react"
import { categoriesAllRequest } from "../../tools/CategoryHelper"
import Category from "./Category"
import Drawer, { type DrawerInfo } from "../layout/Drawer"
import { useNavigate } from "react-router"
import AssetDetails from "../assets/AssetDetails"
import type { Asset } from "../admin/AdminPanels"

export interface CategoryResponse {
    id: number
    name: string
    assets: Asset[]
}

export interface Content {
    name: string
    status: string
}



const Categories = () => {
    const [categories, setCategories] = useState<CategoryResponse[]>([])
    const [drawerInfo, setDrawerInfo] = useState<DrawerInfo | null>(null)
    const [drawerOpen, SetDrawerOpen] = useState(false)

    const [assetDetailsOpen, setAssetDetailsOpen] = useState(false)
    const [selectedAsset, setSelectedAsset] = useState< Asset| null>(null)

    const navigate = useNavigate()

    useEffect(() => {
        handleCategoriesRequest()
    }, [])

    const handleCategoriesRequest = async () => {
        try {
            const response = await categoriesAllRequest()
            setCategories(response)
            
        } catch {
            navigate("/auth")
        }
    }

    const handleDrawerOpen = (id: number) => {
        const category = categories.find(c => c.id == id)
        if (category) {
            setDrawerInfo({
                name: category.name,
                objects: category.assets
            })
        }
        SetDrawerOpen(true)
    }
    const handleDrawerClose = () => SetDrawerOpen(false)

    const handleAssetDetailsClose = () => setAssetDetailsOpen(false)

    const handleAssetClicked = (id: any) => {
        console.log(id)
        const assets = drawerInfo?.objects
        const asset = assets?.find(asset => Object.entries(asset).some(([key, value]) => key === "id" && value === id)) ?? null
        console.log(asset)
        setSelectedAsset(asset as Asset)
        setAssetDetailsOpen(true)
    }
    
    const drawerInfoFilter: Record<string, string> = {
        name: "Name",
        status: "Status"
    }
    return (
        <div className="flex flex-col flex-1 mt-[64px] gap-4 p-5">
            <div className="text-3xl font-bold">
                Categories
            </div>
            <div className="grid grid-5 gap-4">
                {categories.map((c, index) => <Category key={index} name={c.name} onClick={() => handleDrawerOpen(c.id)} />)}
            </div>
            {drawerInfo && <Drawer info={drawerInfo} drawerInfoFilter={drawerInfoFilter} open={drawerOpen} onClose={handleDrawerClose} onElementClicked={handleAssetClicked} />}
            {selectedAsset && <AssetDetails assetData={selectedAsset} open={assetDetailsOpen} onClose={handleAssetDetailsClose} />}
        </div>
    )
}

export default Categories