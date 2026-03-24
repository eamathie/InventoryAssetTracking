import { useEffect, useState } from "react"
import { categoriesAllRequest } from "../../tools/CategoryHelper"
import Category from "./Category"
import Drawer from "../layout/Drawer"
import { useNavigate } from "react-router"

export interface CategoryResponse {
    id: number
    name: string
    assets: Asset[]
}

export interface Content {
    name: string
    status: string
}

export type DrawerInfo = {
    name: string
    content: [string, string][][]
}

export interface Asset {
    id: number
    name: string
    status: string
    purchaseDate: string
    notes: string
    [key: string]: any 
}


const Categories = () => {
    const [categories, setCategories] = useState<CategoryResponse[]>([])
    const [drawerInfo, setDrawerInfo] = useState<DrawerInfo | null>(null)
    const [open, SetOpen] = useState(false)
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

    const handleOpen = (id: number) => {
        const category = categories.find(c => c.id == id)
        if (category) {
            const fields: (keyof typeof category.assets[number])[] = [
                "name",
                "status",
                //"purchaseDate",
                //"notes"
            ]

            setDrawerInfo({
                name: category.name,
                content: category.assets.map(asset =>
                    fields.map(key => [key, String(asset[key])] as [string, string])
                )
            })
        }
        SetOpen(true)
    }

    const handleAssetClicked = () => {
        navigate("/assets")
    }

    const handleClose = () => SetOpen(false)

    return (
        <div className="flex flex-col flex-1 mt-[64px] gap-4 p-5">
            <div className="text-3xl font-bold">
                Categories
            </div>
            <div className="grid grid-5 gap-4">
                {categories.map((c, index) => <Category key={index} name={c.name} onClick={() => handleOpen(c.id)} />)}
            </div>
            {drawerInfo && <Drawer info={drawerInfo} open={open} onClose={handleClose} onElementClicked={handleAssetClicked} />}
        </div>
    )
}

export default Categories