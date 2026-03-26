import { useEffect, useState } from "react"
import { includeItemAttributes } from "../../tools/AttributesExtractor"
import InputField from "../auth/InputField"
import type { ElementToHandle } from "./AdminPanels"
import { categoryPatch } from "../../tools/CategoryHelper"
import { assetPatch } from "../../tools/AssetsHelper"
import { userPatch } from "../../tools/UserHelper"

type EditedAttributes = {
    name: string
    email: string
    status: string
    purchaseDate: string
    categoryId: number
    userId: string
    notes: string
}

const requestFunctions: Record<string, (id: any, data: Record<string, string>) => Promise<any>> = {
  category: categoryPatch,
  asset: assetPatch,
  user: userPatch,
} as const;

type RequestType = keyof typeof requestFunctions;

const EditPanel = ({elementToEdit, open, onClose }: { elementToEdit: ElementToHandle | null, open: boolean, onClose: () => void }) => {
    
    const [attributes, setAttributes] = useState<string[]>([])
    const [type, setType] = useState("")
        
    const [editedAttributes, setEditedAttributes] = useState<EditedAttributes>({
        name: "",
        email: "",
        status: "",
        purchaseDate: "",
        categoryId: 0,
        userId: "",
        notes: ""
    })
     
    const userAttributes = [
        "name",
        "email",
    ]

    const assetAttributes = [
        "name",
        "status",
        "purchaseDate",
        "categoryId",
        "userId",
        "notes"
    ]

    const categoryAttributes = [
        "name"
    ]
    
    useEffect(() => {
        if (!elementToEdit)
            return

        switch (elementToEdit.title) {
            case "user":
            {
                setType("user")
                setAttributes(userAttributes)
                break
            }
            case "asset":
                setType("asset")
                setAttributes(assetAttributes)
                break
            case "category":
                setType("category")
                setAttributes(categoryAttributes)
        }

        console.log("element to edit changed")
    }, [elementToEdit])
    
    const handleEdit = async (id: any) => {

        if (!elementToEdit)
            throw new Error("elementToEdit somehow now received in EditPanel")

        try {
            const data = Object.fromEntries(Object.entries(includeItemAttributes(elementToEdit.element, attributes)))
            const changedOnly = Object.entries(editedAttributes).filter(([key, _]) => {
                return Object.keys(data).includes(key)
            })
            
            const repopulated = changedOnly.map(([key, value]) => {
                if (value === "")
                    value = data[key]

                return [key, value]
            })

            const dataToSend = Object.fromEntries(repopulated)

            const requestFunction = requestFunctions[type as RequestType] // <-- this is probably very scary
            const response = await requestFunction(id, dataToSend)
            console.log(response);

            setEditedAttributes({
                name: "",
                email: "",
                status: "",
                purchaseDate: "",
                categoryId: 0,
                userId: "",
                notes: ""
            })
        } catch (error) {
            console.error(error);
        }
        console.log(`Booom, ${elementToEdit?.title} ${id} just got edited`)

    }

    const handleFieldChange = (field: string, value: string) => {
        setEditedAttributes(prev => ({
            ...prev,
            [field.toLowerCase()]: value
        }))
    }
    
    if (!open) return null
    return (
        <div className="flex flex-col gap-3 rounded-lg shadow-xl border-2 border-gray-400 self-center justify-self-center max-h-100 w-100 p-3 px-5 bg-white fixed inset-0" aria-labelledby="slide-over-title" role="dialog" aria-modal="true">
            <h2 className="self-center text-xl font-medium bold">Currently editing {elementToEdit?.title} {elementToEdit?.element.id}</h2>

            <div className="text-base flex flex-col items-stretch rounded-md border-2 border-gray-300 self-center overflow-y-scroll w-full px-5 py-5">
                {elementToEdit?.element && Object.entries(
                    includeItemAttributes(elementToEdit?.element, attributes))
                    .map(([key, value]) => (
                        <InputField key={key} name={key} defaultValue={value} onChange={handleFieldChange} />
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