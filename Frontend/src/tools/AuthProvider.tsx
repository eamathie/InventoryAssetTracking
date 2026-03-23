import { createContext, useEffect, useState } from "react";
import { userRoles } from "./UserHelper";


interface AuthProviderProps {
    children: React.ReactNode
}

interface AuthContextType {
    roles: string[]
    refreshRoles: () => Promise<void>
}

export const AuthContext = createContext<AuthContextType | null>(null)

export const AuthProvider = ({ children }: AuthProviderProps) => {
    const [roles, setRoles] = useState<string[]>([])

    const refreshRoles = async () => {
        const r = await userRoles()
        setRoles(r)
    }

    useEffect(() => {
        refreshRoles()
    }, [])

    return (
        <AuthContext.Provider value={{ roles, refreshRoles}}>
            {children}
        </AuthContext.Provider>
    )
}