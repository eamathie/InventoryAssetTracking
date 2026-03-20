import { useEffect, useState } from "react"
import type { AssetResponse } from "../assets/Assets"
import { categoriesAllRequest } from "../../tools/CategoryHelper"
import Category from "./Category"
import Drawer from "../layout/Drawer"

export type CategoryResponse = {
    id: number
    name: string
    assets: AssetResponse[]
}

type DrawerInfo = {
    name: string
    content: Object[]//[string, string | number | Date][]
}

const Categories = () => {
    const [categories, setCategories] = useState<CategoryResponse[]>([])
    const [drawerInfo, setDrawerInfo] = useState<DrawerInfo | null>(null)
    const [open, SetOpen] = useState(false)

    useEffect(() => {
        handleCategoriesRequest()
    }, [])

    const handleCategoriesRequest = async () => {
        const response = await categoriesAllRequest()
        setCategories(response)
    }

    const handleOpen = (id: number) => {
        getCategoryAssets(id)
        const category = categories.find(c => c.id == id)
        if (category) {
            setDrawerInfo({
                name: category.name,
                content: category.assets
            })
        }
        SetOpen(true)
    }

    const getCategoryAssets = (id: number) => {
        console.log(categories.find(c => c.id == id)?.assets)
    }

    const handleClose = () => SetOpen(false)

    return (
        <div className="min-h-screen bg-gradient-to-r from-blue-400 to-purple-500 justify-center">
            <div className="flex flex-col gap-4 p-5">
                <div className="text-3xl font-bold">
                    Categories
                </div>
                <div className="grid grid-5 gap-4">
                    {categories.map((c, index) => <Category key={index}
                    id={c.id}
                    name={c.name}
                    assets={c.assets}
                    onClick={() => handleOpen(c.id)}
                    />)
                    }
                </div>
                {drawerInfo && <Drawer title={drawerInfo.name} content={drawerInfo.content} open={open} onClose={handleClose}  />}
            </div>
        </div>
    )
}

export default Categories