interface InputFieldProps {
    name: string;
    defaultValue: string
    onChange: (field: string, value: string) => void;
}

const InputField = ({ name, defaultValue, onChange }: InputFieldProps) => {
    return(
        <div className="flex flex-col justify-items-stretch p-1">
            <label className="flex flex-row justify-evenly gap-15 whitespace-nowrap text-sm"></label>
                <h2>{name}:</h2>
                <input 
                    className="flex-1 max-w-70 min-w-0 border border-solid rounded px-1"
                    defaultValue={defaultValue}
                    onChange={(e) => onChange(name, e.target.value)}
                    type={name}
                />
        </div>
    )
}

export default InputField;