import InputField from "./InputField";

interface InputFieldsProps {
    names: string[];
}

const InputFields = ({ names }: InputFieldsProps) => {
    return(
        <div className="flex-col">
            {names.map((n, index) => <InputField key={n + index} name={n}/>)}
        </div>
    )
}

export default InputFields