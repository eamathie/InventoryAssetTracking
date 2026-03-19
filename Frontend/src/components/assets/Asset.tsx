import { useEffect, useState } from "react"
import { userById } from "../../tools/UserHelper"

interface AssetProps {
    name: string
    categoryId: number
    status: string
    purchaseDate: Date
    userId: string
    notes: string
}

type User = {
    id: string
    name: string
    email: string
}


const Asset = ({ name, categoryId, status, purchaseDate, userId, notes }: AssetProps) => {

    const [user, setUser] = useState<User | null>(null)

    useEffect(() => {
        handleRequest()
    }, [])

    const handleRequest = async () => {
        const response = await userById(userId)
        setUser(response)
    }

    return(
        <div className="text-sm rounded-lg shadow-xl px-6 py-6 max-w-3xs flex-col cursor-pointer w-full bg-white hover:bg-gray-200">
            <div className="text-xl font-bold underline">
                {name}
            </div>

            <div>
                Status: {status}
            </div>

            <div>
                Assigned to: {user?.name}
            </div>

            <div>
                Purchased: {purchaseDate.toString()}
            </div>

            <div className="italic py-1">
                Notes: {notes}
            </div>
        </div>
    )
}

export default Asset