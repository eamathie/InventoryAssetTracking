import { useContext, useState } from "react";
import InputFields from "./InputFields";
import { authRequest } from "../../tools/AuthHelper";
import { useNavigate } from "react-router";
import { AuthContext } from "../../tools/AuthProvider";

type UserInfo = {
    name: string
    email: string
    password: string
}

const RegisterLoginUser = () => {
    const auth = useContext(AuthContext)
    const fieldsRegister = ["Name", "Email", "Password"]
    const fieldsLogin = ["Email", "Password"]
    const [register, setRegister] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");

    const [userInfo, setUserInfo] = useState<UserInfo>({
        name: "",
        email: "",
        password: ""
    })

    const handleFieldChange = (field: string, value: string) => {
        setUserInfo(prev => ({
            ...prev,
            [field.toLowerCase()]: value
        }))
    }

    const navigate = useNavigate();

    const handleSubmit = async () => {
        try {
            const relativeUrl = register ? "../Auth/register" : "../Auth/login"
            const combinedUrl = new URL(relativeUrl, import.meta.env.VITE_BACKEND_BASE_URL_HTTPS).href
    
            const filtered = Object.fromEntries(
                Object.entries(userInfo).filter(([_, value]) => value !== "")
            )
    
            const response = await authRequest(combinedUrl, filtered)
            console.log(response);
    
            setUserInfo({
                name: "",
                email: "",
                password:""
            })

            setErrorMessage("")
            
            await auth?.refreshRoles();
            
            navigate("/categories")
            
        } catch (error) {
            setErrorMessage("Something went wrong, please try again");
        }
    }

    return(
        <div className="min-h-screen bg-gradient-to-r from-blue-400 to-purple-500 flex items-center justify-center">
            <div className="bg-white rounded-lg shadow-xl px-10 py-8 max-w-xs flex-col">
                <div>
                    <h1 className="text-3xl font-bold text-gray-800 mb-4">{ register ?  "Register user" : "Log in"}</h1>
                    <p className="text-sm text-gray-600 mb-6">Please enter required information below</p>
                </div>

                <form className="flex flex-col gap-3" onSubmit={(e) => {
                    e.preventDefault()
                    handleSubmit()
                    }}
                    >
                    <InputFields names={register ? fieldsRegister : fieldsLogin} onChange={handleFieldChange} />
                    <button type="submit" className="cursor-pointer w-full bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded transition-colors">
                    {register ? "Register" : "Log in"}
                    </button >

                    <div className="text-xs text-red-500 font-bold rounded transition-colors w-fit">
                        {errorMessage}
                    </div>

                    <div onClick={() => setRegister(!register)} className="cursor-pointer text-xs text-blue-500 font-bold rounded transition-colors w-fit">
                        {register ? "Already have an account?" : "Don't have an account?"}
                    </div>
                </form>
            </div>
        </div>
    )
}

export default RegisterLoginUser;