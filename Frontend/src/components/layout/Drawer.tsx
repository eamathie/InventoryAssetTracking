import type { DrawerInfo } from "../categories/Categories"

const Drawer = ({ info, open , onClose, onElementClicked }: { info: DrawerInfo, open: boolean, onClose: () => void, onElementClicked: () => void }) => {           
    if (!open) return null
    return(
        <div className="pointer-events-none fixed inset-0 overflow-hidden" aria-labelledby="slide-over-title" role="dialog" aria-modal="true">
            <div className="absolute inset-0 overflow-hidden">
                <div className="absolute inset-0 bg-opacity-75 transition-opacity"></div>
                <div className="fixed inset-y-0 right-0 pl-10 max-w-full flex">
                    <div className="shadow-xl relative w-screen max-w-md">
                        <div className="pointer-events-auto h-full flex flex-col py-6 bg-white shadow-xl overflow-y-scroll">
                            <div className="px-4 sm:px-6">
                                <div className="flex items-start justify-between">
                                    <h1 id="slide-over-title" className="text-2xl font-medium bold underline text-gray-900">{info.name}</h1>
                                    <div className="ml-3 h-7 flex items-center">
                                    <button onClick={onClose} type="button" className="cursor-pointer bg-white rounded-md text-gray-400 hover:text-gray-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
                                        <span className="sr-only">Close panel</span>
                                        {/* Heroicon name: outline/x */}
                                        <svg className="h-6 w-6" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" aria-hidden="true">
                                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 18L18 6M6 6l12 12"/>                                            
                                        </svg>
                                    </button>
                                    </div>
                                </div>
                            </div>
                            <div className="mt-6 relative flex-1 px-4 sm:px-6">
                            {/* Replace with your content */}
                            <div className="absolute inset-0 px-4 sm:px-6">
                                <div className="flex flex-col items-start justify-start gap-3 h-full border-2 border-dashed border-gray-200" aria-hidden="true">
                                    
                                    {/*Here goes the content*/}
                                    {/* <div className="w-3xs h-3xs border-2">
                                        {qrCode && <img src={`data:${qrCode.contentType};base64,${qrCode.fileContents}`} alt="QR Code" />}
                                    </div> */}
                                        {info.content.map((card, i) => (
                                            <div key={i} className="w-full shadow-md mb-4 p-3 border rounded bg-white cursor-pointer hover:bg-gray-100">
                                                {card.map(([key, value], index) => (
                                                    <div onClick={onElementClicked} key={index} className="flex py-1" >
                                                        <span className="font-semibold">{key}:</span>
                                                        <span className="pl-5">{value}</span>
                                                    </div>
                                                ))}
                                            </div>
                                        ))}
                                </div>
                            </div>
                            {/* End replace */}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
} 

export default Drawer