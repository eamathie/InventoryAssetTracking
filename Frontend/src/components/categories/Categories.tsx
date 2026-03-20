import { useEffect, useState } from "react"
import { categoriesAllRequest } from "../../tools/CategoryHelper"
import Category from "./Category"
import Drawer from "../layout/Drawer"

export interface CategoryResponse {
    id: number
    name: string
    assets: Content[]
}

export interface Content {
    name: string
    status: string
}

export type DrawerInfo = {
    name: string
    content: Content[]
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
        const category = categories.find(c => c.id == id)
        if (category) {
            setDrawerInfo({
                name: category.name,
                content: category.assets.map((c: Content) => ({ name: c.name, status: c.status }))
            })
        }
        SetOpen(true)
    }

    const handleClose = () => SetOpen(false)

    return (
        <div className="min-h-screen bg-gradient-to-r from-blue-400 to-purple-500 justify-center">
            <div className="flex flex-col gap-4 p-5">
                <div className="text-3xl font-bold">
                    Categories
                </div>
                <div className="grid grid-5 gap-4">
                    {categories.map((c, index) => <Category key={index} name={c.name} onClick={() => handleOpen(c.id)} />)}
                </div>
                {drawerInfo && <Drawer drawerInfo={drawerInfo} open={open} onClose={handleClose}  />}
            </div>
        </div>
    )
}

export default Categories