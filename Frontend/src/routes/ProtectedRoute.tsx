import { Navigate, Outlet } from "react-router"
import { useAuth } from "../tools/AuthProvider"

export const ProtectedRoute = () => {
    const { token } = useAuth()!;  

    if (!token)
        return <Navigate to="/auth" />
    
    return <Outlet />
}