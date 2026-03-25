import InputField from "./InputField";

interface InputFieldsProps {
    names: readonly string[];
    onChange: (field: string, value: string) => void;
}

const InputFields = ({ names, onChange}: InputFieldsProps) => {
    return(
        <div className="flex-col">
            {names.map((name) => <InputField defaultValue="" key={name} name={name} onChange={onChange}/>)}
        </div>
    )
}

export default InputFields