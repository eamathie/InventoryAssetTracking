import { useNavigate } from "react-router"
import { useContext, useEffect, useState } from "react"
import { AuthContext } from "../../tools/AuthProvider"
import AdminPanel from "./AdminPanel"
import { usersAll } from "../../tools/UserHelper"
import { assetsAllRequest } from "../../tools/AssetsHelper"
import { categoriesAllRequest } from "../../tools/CategoryHelper"

export type User = {
    id: string
    name: string
    email: string
    createdAt: string
    assets: Asset[]
    checkouts: Checkout[]
}

export type Asset = {
    id: number
    name: string
    status: string
    categoryId: number
    purchaseDate: Date
    qrCodePath: string
    userId: string
    notes: string
}

type Category = {
    id: number
    name: string
    assets: Asset[]
}

type Checkout = {
    id: number
    userId: string
    assetId: number
    checkedOutAt: string
    checkedInAt: string
}

export type PanelInfo = {
    name: string
    content: [string, string][][]
}

const AdminPanels = () => {
    const navigate = useNavigate()
    
    const auth = useContext(AuthContext)
    const roles = auth?.roles
    const admin = roles?.includes("Admin")

    const [users, setUsers] = useState<User[]>([])
    const [assets, setAssets] = useState<Asset[]>([])
    const [categories, setCategories] = useState<Category[]>([])
    
    useEffect(() => {
        if (roles === null)
            return
        if (!admin)
            navigate("/auth")
    }, [roles, admin])

    useEffect(() => {
        if (admin)
            handleRequests()
    }, [admin])

    const handleRequests = async() => {
        try {
            const usersResponse = await usersAll()
            const assetsResponse = await assetsAllRequest()
            const categories = await categoriesAllRequest()
            setUsers(usersResponse)
            setAssets(assetsResponse)
            setCategories(categories)
        } catch (error) {
            console.log(error)
        }
    }

    const excludedKeys = ["assets", "checkouts", "categoryId"]
    return (
        <div className="flex flex-col flex-1 max-h-[494px] mt-[64px] px-6 py-3 gap-2">
            <h1 className="text-3xl font-bold">Admin page</h1>
                <div className="flex flex-row items-stretch gap-2 justify-center items-center max-h-full w-full pb-8 ">
                    <AdminPanel title="Users" content={users.map(u => Object.fromEntries(Object.entries(u).filter(([key]) => !excludedKeys.includes(key))))} />
                    <AdminPanel title="Assets" content={assets.map(a => Object.fromEntries(Object.entries(a).filter(([key]) => !excludedKeys.includes(key))))} />
                    <AdminPanel title="Categories" content={categories.map(c => Object.fromEntries(Object.entries(c).filter(([key]) => !excludedKeys.includes(key))))} />
               </div>
        </div>
    )
}

export default AdminPanels


