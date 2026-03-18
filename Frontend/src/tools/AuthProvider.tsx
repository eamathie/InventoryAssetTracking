import axios from "axios";
import { createContext, useContext, useEffect, useMemo, useState } from "react"

type AuthContextType = {
    token: string | null
    setToken: React.Dispatch<React.SetStateAction<string | null>>
}
const AuthContext = createContext<AuthContextType | null>(null);

type Props = {
    children: React.ReactNode
}

/**
 * based on this guide: https://dev.to/sanjayttg/jwt-authentication-in-react-with-react-router-1d03
 */
const AuthProvider = ({ children }: Props) => {
    const [token, setToken] = useState<string | null>(localStorage.getItem("token"))

    useEffect(() => {
        if (token) {
            axios.defaults.headers.common["Authorization"] = "Bearer " + token
            localStorage.setItem('token', token)
        } else {
            delete axios.defaults.headers.common["Authorization"]
            localStorage.removeItem('token')
        }
    }, [token])

    const contextValue = useMemo(
        () => ({
            token,
            setToken,
        }), 
        [token]
    )

    return (
        <AuthContext.Provider value={contextValue}>
            {children}
        </AuthContext.Provider>
    )
}

export const useAuth = () => {
    return useContext(AuthContext);
}

export default AuthProvider;