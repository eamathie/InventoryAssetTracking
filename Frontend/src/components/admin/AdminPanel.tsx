export type AdminPanelInfo = {
    title: string
    content: { [k: string]: any; }[]
}

const AdminPanel = ({ title, content }: AdminPanelInfo) => {
    return (
        <div className="rounded-lg shadow-xl p-3 w-full max-h-full bg-white overflow-y-scroll">
            <h1 className="text-xl font-medium bold underline text-gray-900">{title}</h1>
            <div className="flex flex-col gap-4">
                {content.map((obj, index) =>
                    <div key={index} className="rounded-lg shadow-xl p-3 border-2 border-gray-300"> 
                        {Object.entries(obj).map(([key, value]) => (
                            <h2 key={key}>{key}: {value}</h2>
                        ))}
                    </div>
                )}
            </div>
        </div>
    )
}

export default AdminPanel