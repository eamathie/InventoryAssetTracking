import { useNavigate } from "react-router"
import { useContext, useEffect, useState } from "react"
import { AuthContext } from "../../tools/AuthProvider"
import AdminPanel from "./AdminPanel"
import { usersAll } from "../../tools/UserHelper"
import { assetsAllRequest } from "../../tools/AssetsHelper"
import { categoriesAllRequest } from "../../tools/CategoryHelper"
import DeleteConfirmPanel from "./DeleteConfirmPanel"
import EditPanel from "./EditPanel"
import { checkoutsAllRequest } from "../../tools/CheckoutsHelper"

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

export type Category = {
    id: number
    name: string
    assets: Asset[]
}

export type Checkout = {
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

export type ElementToHandle = {
    title: string
    element: User| Asset | Category | Checkout
}

const AdminPanels = () => {
    const navigate = useNavigate()
    
    const auth = useContext(AuthContext)
    const roles = auth?.roles
    const admin = roles?.includes("Admin")

    const [users, setUsers] = useState<User[]>([])
    const [assets, setAssets] = useState<Asset[]>([])
    const [categories, setCategories] = useState<Category[]>([])
    const [checkouts, setCheckouts] = useState<Checkout[]>([])

    const [deleteConfirmOpen, setDeleteConfirmOpen] = useState(false)
    const [elementToHandle, setElementToHandle] = useState<ElementToHandle | null>(null)
    const [editOpen, setEditOpen] = useState(false)
    
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
            const checkouts = await checkoutsAllRequest()
            setUsers(usersResponse)
            setAssets(assetsResponse)
            setCategories(categories)
            setCheckouts(checkouts)
        } catch (error) {
            console.log(error)
        }
    }

    const getElementByIdAndType = (id: any, title: string) => {
        let obj
        if (title === "user")
            obj = users.find(u => u.id === id)
        else if (title === "asset")
            obj = assets.find(a => a.id === id)
        else if (title === "category")
            obj = categories.find(c => c.id === id)
        else
            obj = checkouts.find(c => c.id === id)

        return obj
    }   

    const handleDeleteConfirmOpen = (id: any, title: string) => {
        const element = getElementByIdAndType(id, title)
        if (!element)
            throw new Error(`Could not find ${title} with id ${id}`)
        setElementToHandle({title, element})
        setDeleteConfirmOpen(true)
    }
    
    
    const handleEditOpen = (id: any, title: string) => {
        const element = getElementByIdAndType(id, title)
        if (!element)
            throw new Error(`Could not find ${title} with id ${id}`)
        setElementToHandle({title, element})
        setEditOpen(true)
    }
    
    const handleDeleteConfirmClose = () => setDeleteConfirmOpen(false)
    const handleEditClose = () => setEditOpen(false)

    const panels: [string, (User[] | Asset[] | Category[] | Checkout[])][] = [
        ["Users", users],
        ["Assets", assets],
        ["Categories", categories],
        ["Checkouts", checkouts]
    ]

    return (
        <div className="flex flex-col flex-1 max-h-[494px] mt-[64px] px-6 gap-2">
            <h1 className="text-3xl font-bold">Admin page</h1>
            <div className="flex gap-2 justify-center max-h-full w-full pb-6">
                {panels.map((p, index) => 
                    <AdminPanel 
                        key={index} 
                        title={p[0]}  
                        content={p[1]} 
                        onEditClicked={handleEditOpen} 
                        onDeleteClicked={handleDeleteConfirmOpen}
                    />
                )}
            </div>
            <DeleteConfirmPanel elementToDelete={elementToHandle} open={deleteConfirmOpen} onClose={handleDeleteConfirmClose} />
            <EditPanel elementToEdit={elementToHandle} open={editOpen} onClose={handleEditClose} />
        </div>
    )
}

export default AdminPanels


