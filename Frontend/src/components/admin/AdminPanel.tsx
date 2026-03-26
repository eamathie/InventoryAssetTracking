import { filterItemAttributes } from "../../tools/AttributesExtractor"
import type { Asset, Category, User } from "./AdminPanels"

export type AdminPanelInfo = {
    title: string
    content: User[] | Category[] | Asset[]
    onEditClicked: (id: any, title: string) => void
    onDeleteClicked: (id: any, title: string) => void
}

// used for converting for message in pop-up ("you sure you want to edit/delete 'user'..." instead of 'users' etc.)
const displayNames: Record<string, string> = {
    "Users": "user",
    "Assets": "asset",
    "Categories": "category"
}

const excludedKeys = ["assets", "checkouts"]
const AdminPanel = ({ title, content, onEditClicked, onDeleteClicked }: AdminPanelInfo) => {
    return (
        <div className="rounded-lg shadow-xl p-3 w-full max-h-full bg-white overflow-y-scroll">
            <h1 className="text-xl font-medium bold underline text-gray-900">{title}</h1>
            <div className="flex flex-col gap-4">
                {content.map(item => filterItemAttributes(item, excludedKeys)).map((obj) =>
                    <div key={obj.id} className="rounded-lg shadow-xl w-full px-5 py-4 border-2 border-gray-300"> 
                        <div className="flex justify-start w-full gap-4 text-sm">
                            <div className="shrink-0">
                                {Object.entries(obj).map(([key, _]) => (
                                        <h2 className="font-bold">{key}: </h2>
                                ))} 
                            </div>
                            <div className="flex-1 min-w-0 font-normal ">
                                {Object.entries(obj).map(([_, value]) => (
                                        <h2 className="truncate">{value} </h2>
                                ))}   
                            </div>
                        </div>
                        
                        <div className="flex flex-row gap-10 justify-start pl-3 py-2">
                            <button onClick={() => onEditClicked(obj.id, displayNames[title])} className="rounded-md bg-indigo-600 px-2 py-1 text-sm font-medium text-white hover:bg-indigo-700">
                                Edit
                            </button>
                            <button onClick={() => onDeleteClicked(obj.id, displayNames[title])} className="rounded-md bg-red-600 px-2 py-1 text-sm font-medium text-white hover:bg-red-700">
                                Delete
                            </button>
                        </div>
                    </div>
                )}
            </div>
        </div>
    )
}

export default AdminPanel