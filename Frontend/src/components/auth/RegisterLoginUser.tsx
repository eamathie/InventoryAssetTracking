import { useState } from "react";
import InputFields from "./InputFields";

type UserInfo = {
    name: string
    email: string
    password: string
}

const RegisterLoginUser = () => {

    const fieldsRegister = ["Name", "Email", "Password"]
    const fieldsLogin = ["Email", "Password"]
    const [register, setRegister] = useState(false);

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

    const handleSubmit = async () => {
        const regUrl = "../Auth/register"
        const loginUrl = "../Auth/login"
        const relUrl = register ? regUrl : loginUrl
        const combinedUrl = new URL(relUrl, import.meta.env.VITE_BACKEND_BASE_URL).href
        console.log(combinedUrl)

        const filtered = Object.fromEntries(
            Object.entries(userInfo).filter(([_, value]) => value !== "")
        )

        console.log(filtered)

        try {
            const response = await fetch(combinedUrl, {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(filtered)
            })
            if (!response.ok) {
                throw new Error(`Response status: ${response.status}`)
            }
        } catch (error) {
            console.error(error)
        }

        setUserInfo({
            name: "",
            email: "",
            password:""
        })
    }

    return(
      <div className="bg-white rounded-lg shadow-xl px-10 py-8 max-w-xs flex-col">
        <div>
            <h1 className="text-3xl font-bold text-gray-800 mb-4">{ register ?  "Register user" : "Log in"}</h1>
            <p className="text-sm text-gray-600 mb-6">Please enter required information below</p>
        </div>

        <form className="flex flex-col gap-3" action={handleSubmit}>
            <InputFields names={register ? fieldsRegister : fieldsLogin} onChange={handleFieldChange} />
            <button type="submit" className="cursor-pointer w-full bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded transition-colors">
            {register ? "Register" : "Log in"}
            </button >

            <div onClick={() => setRegister(!register)} className="cursor-pointer text-xs text-blue-500 font-bold rounded transition-colors w-fit">
                {register ? "Already have an account?" : "Don't have an account?"}
            </div>
        </form>
      </div>
    )
}

export default RegisterLoginUser;