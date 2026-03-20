import { useEffect, useState } from "react"
import type { AssetResponse } from "./Assets"
import { qrCodeRequest } from "../../tools/QrCodeHelper"
import { userById } from "../../tools/UserHelper"

type QrCodeResponse = {
    contentType: string
    enableRangeProcessing: boolean
    entityTag: string | null
    fileContents: string
    fileDownloadName: string | null
    lastModified: string | null
}

type User = {
    id: string
    name: string
    email: string
}

const AssetDetails = ( { assetData, open , onClose}: {assetData: AssetResponse | null, open: boolean, onClose: () => void }) => {
    const [qrCodePath, setQrCodePath] = useState<string | null>(null)
    const [qrCode, setQrCode] = useState<QrCodeResponse | null>(null)
    const [user, setUser] = useState<User | null>(null)

    
    useEffect(() => {
        if (assetData) {
            setQrCodePath(assetData.qrCodePath)
            handleUserRequest(assetData.userId)
        }

    }, [assetData])
    
    useEffect(() => {
        const num = numberInString(qrCodePath) 
        handleQrResponse(num)
    }, [qrCodePath])
    
    const handleQrResponse = async (num: number | null) => {
        const response = num && await qrCodeRequest(num)
        setQrCode(response)
    }

    const handleUserRequest = async (userId: string) => {
        const response = await userById(userId)
        setUser(response)
    }

    if (!open) return null
    return(
        <div className="fixed inset-0 overflow-hidden" aria-labelledby="slide-over-title" role="dialog" aria-modal="true">
            <div className="absolute inset-0 overflow-hidden">
                <div className="absolute inset-0 bg-opacity-75 transition-opacity"></div>
                    <div className="fixed inset-y-0 right-0 pl-10 max-w-full flex">
                        <div className="shadow-xl relative w-screen max-w-md">
                            <div className="h-full flex flex-col py-6 bg-white shadow-xl overflow-y-scroll">
                                <div className="px-4 sm:px-6">
                                    <div className="flex items-start justify-between">
                                        <h1 id="slide-over-title" className="text-2xl font-medium bold underline text-gray-900">{assetData?.name}</h1>
                                        <div className="ml-3 h-7 flex items-center">
                                        <button onClick={onClose} type="button" className="bg-white rounded-md text-gray-400 hover:text-gray-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
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
                                    <div className="flex flex-col items-center justify-center gap-3 h-full border-2 border-dashed border-gray-200" aria-hidden="true">
                                        
                                        {/*Here goes the content*/}
                                        <div className="w-3xs h-3xs border-2">
                                            {qrCode && <img src={`data:${qrCode.contentType};base64,${qrCode.fileContents}`} alt="QR Code" />}
                                        </div>
                                        <div>
                                            <h3>Status: {assetData?.status}</h3>
                                            <h3>Assigned to: {user?.name}</h3>
                                            <h3>Purchased: {assetData?.purchaseDate.toString()}</h3>
                                            <h3>Notes: {assetData?.notes}</h3>
                                        </div>

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

export default AssetDetails

/**
 * Find number (index) in qr code path
 * @param str 
 * @returns 
 */
const numberInString = (str: string | null): number | null => {
    if (!str) 
        return null

    const match = str.match(/-?\d+(\.\d+)?/)
    if (!match) 
        return null

    return Number(match[0])
}