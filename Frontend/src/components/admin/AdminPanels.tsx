import { useNavigate } from "react-router"
import { useContext, useEffect } from "react"
import { AuthContext } from "../../tools/AuthProvider"

const AdminPanels = () => {
    const navigate = useNavigate()

    const auth = useContext(AuthContext)

    const roles = auth?.roles
    const admin = roles?.includes("Admin")

    
    useEffect(() => {
        if (roles === null)
            return
        if (!admin)
            navigate("/auth")
    }, [roles, admin])

    return (
        <div className="min-h-screen bg-gradient-to-r from-blue-400 to-purple-500 justify-center">
            <div className="max-w bg-red">

            </div>
            <h1>Admin page</h1>
        </div>
    )
}

export default AdminPanels