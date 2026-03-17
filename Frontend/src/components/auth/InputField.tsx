
interface InputFieldProps {
    name: string;
}

const InputField = ({ name }: InputFieldProps) => {
    return(
        <div className="grid grid-cols-[auto_1fr] justify-items-stretch p-1">
            <label className="whitespace-nowrap text-sm">
                {name}:
            </label>
            <input className="flex-1 max-w-40 min-w-0 justify-self-end border border-solid rounded px4"/>
        </div>
    )
}

export default InputField;