import type { User, Asset, Category } from "./AdminPanels"

const DeleteConfirmPanel = ({ elementToDelete, open, onClose }: { elementToDelete:  User | Asset | Category | null, open: boolean, onClose: () => void }) => {
    const handleDelete = async (id: any) => {
        console.log(`Booom, element ${id} just got nuked`)
    }
    
    if (!open) return null
    return (
        <div className="flex flex-col gap-3 rounded-lg shadow-xl border-2 border-gray-400 self-center justify-self-center h-50 w-70 p-3 bg-white fixed inset-0 overflow-hidden" aria-labelledby="slide-over-title" role="dialog" aria-modal="true">
            <h2 className="text-xl font-medium bold">Are you sure you want to delete this {elementToDelete?.type}?</h2>
            <h2 className="self-center text-xl font-medium bold underline">It is irreversible!</h2>

            <div className="self-center flex flex-row gap-20 p-3">
                <button onClick={onClose} className="rounded-md bg-indigo-600 px-2 py-1 text-sm font-medium text-white hover:bg-indigo-700">
                    Cancel
                </button>
                <button onClick={() => handleDelete(elementToDelete?.id)} className="rounded-md bg-red-600 px-2 py-1 text-sm font-medium text-white hover:bg-red-700">
                    Delete
                </button>
            </div>
        </div>
    )
}

export default DeleteConfirmPanel