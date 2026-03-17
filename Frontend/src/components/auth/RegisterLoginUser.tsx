import { useState } from "react";
import InputFields from "./InputFields";

const RegisterLoginUser = () => {

    const fieldsRegister = ["Name", "Email", "Password"]
    const fieldsLogin = ["Email", "Password"]
    const [register, setRegister] = useState(true);


    const handleRegister = () => {
        const str = register === true ? "registered" : "logged in"
        console.log(str);
    }

    return(
      <div className="bg-white rounded-lg shadow-xl px-10 py-8 max-w-xs flex-col">
        <div>
            <h1 className="text-3xl font-bold text-gray-800 mb-4">{ register ?  "Register user" : "Log in"}</h1>
            <p className="text-sm text-gray-600 mb-6">Please enter required information below</p>
        </div>

        <form className="flex flex-col gap-8" action={handleRegister}>
            <InputFields names={register ? fieldsRegister : fieldsLogin} />
            <button type="submit" className="w-full bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded transition-colors">
            Register
            </button>
        </form>
      </div>
    )
}

export default RegisterLoginUser;