interface InputFieldProps {
    name: string;
    onChange: (field: string, value: string) => void;
}

const InputField = ({ name, onChange }: InputFieldProps) => {
    return(
        <div className="grid grid-cols-[auto_1fr] justify-items-stretch p-1">
            <label className="whitespace-nowrap text-sm">
                {name}:
            </label>
            <input 
                className="flex-1 max-w-40 min-w-0 justify-self-end border border-solid rounded px4"
                onChange={(e) => onChange(name, e.target.value)}
                type={name}
            />
        </div>
    )
}

export default InputField;