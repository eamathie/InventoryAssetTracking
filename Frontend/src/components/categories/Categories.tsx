import { useEffect, useState } from "react"
import type { AssetResponse } from "../assets/Assets"
import { categoriesAllRequest } from "../../tools/CategoryHelper"
import Category from "./Category"

export type CategoryResponse = {
    id: number
    name: string
    assets: AssetResponse[]
}

const Categories = () => {
    const [categories, setCategories] = useState<CategoryResponse[]>([])
    useEffect(() => {
        handleCategoriesRequest()
    }, [])

    const handleCategoriesRequest = async () => {
        const response = await categoriesAllRequest()
        setCategories(response)
    }

    useEffect(() => {
        console.log(categories)
    }, [categories])

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
                    />)
                    }
                </div>
            </div>
        </div>
    )
}

export default Categories