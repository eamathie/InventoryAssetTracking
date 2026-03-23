import { createContext, useEffect, useState } from "react";
import { userRoles } from "./UserHelper";


interface AuthProviderProps {
    children: React.ReactNode
}

interface AuthContextType {
    roles: string[] | null
    refreshRoles: () => Promise<void>
}

export const AuthContext = createContext<AuthContextType | null>(null)

export const AuthProvider = ({ children }: AuthProviderProps) => {
    const [roles, setRoles] = useState<string[] | null>(null)

    const refreshRoles = async () => {
        const r = await userRoles()
        setRoles(r)
    }

    useEffect(() => {
        refreshRoles()
    }, [])

    return (
        <AuthContext value={{ roles, refreshRoles}}>
            {children}
        </AuthContext>
    )
}