
export const qrCodeRequest = async (qrCodeId: number) => {
    const url = new URL(`../Qrcode/${qrCodeId}`, import.meta.env.VITE_BACKEND_BASE_URL_HTTPS).href
    const response = await fetch(url, {
        method: 'GET',
        credentials: 'include'
    })

    if (!response.ok) {
        throw new Error(`Response status: ${response.status}`)
    }

    return response.json()
}