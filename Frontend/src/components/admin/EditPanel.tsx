import { useEffect, useState } from "react"
import { includeItemAttributes } from "../../tools/AttributesExtractor"
import InputField from "../auth/InputField"
import type { ElementToHandle } from "./AdminPanels"

const EditPanel = ({elementToEdit, open, onClose }: { elementToEdit: ElementToHandle | null, open: boolean, onClose: () => void }) => {
    
    const [attributes, setAttributes] = useState<string[]>([])
    
    useEffect(() => {
        if (!elementToEdit)
            return
        switch (elementToEdit.title) {
            case "user":
                setAttributes(userAttributes)
                break
            case "asset":
                setAttributes(assetAttributes)
                break
            case "category":
                setAttributes(categoryAttributes)
        }
    }, [elementToEdit])
        
    const handleEdit = async (id: any) => {
        console.log(`Booom, ${elementToEdit?.title} ${id} just got edited`)
    }
    const userAttributes = [
        "name",
        "email",
    ]

    const assetAttributes = [
        "name",
        "status",
        "purchaseDate",
        "userId",
        "notes"
    ]

    const categoryAttributes = [
        "name"
    ]


    
    if (!open) return null
    return (
        <div className="flex flex-col gap-3 rounded-lg shadow-xl border-2 border-gray-400 self-center justify-self-center max-h-100 w-100 p-3 px-5 bg-white fixed inset-0" aria-labelledby="slide-over-title" role="dialog" aria-modal="true">
            <h2 className="self-center text-xl font-medium bold">Currently editing {elementToEdit?.title} {elementToEdit?.element.id}</h2>

            <div className="text-base flex flex-col items-stretch rounded-md border-2 border-gray-300 self-center overflow-y-scroll w-full px-5 py-5">
                {elementToEdit?.element && Object.entries(
                    includeItemAttributes(elementToEdit?.element, attributes))
                    .map(([key, value]) => (
                        <InputField key={key} name={key} defaultValue={value} onChange={() => console.log("ey")} />
                    ))
                }
            </div>

            <div className="self-center flex flex-row gap-20 p-3">
                <button onClick={onClose} className="rounded-md bg-indigo-600 px-2 py-1 text-sm font-medium text-white hover:bg-indigo-700">
                    Cancel
                </button>
                <button onClick={() => handleEdit(elementToEdit?.element.id)} className="rounded-md bg-red-600 px-2 py-1 text-sm font-medium text-white hover:bg-red-700">
                    Confirm
                </button>
            </div>
        </div>
    )
}

export default EditPanel