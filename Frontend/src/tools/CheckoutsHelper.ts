export const checkoutsAllRequest = async () => {
    const url = new URL("../Checkout", import.meta.env.VITE_BACKEND_BASE_URL_HTTPS).href
    const response = await fetch(url, {
        method: 'GET',
        credentials: 'include'
    })

    if (!response.ok) {
        throw new Error(`Response status: ${response.status}`)
    }

    return response.json()
} 