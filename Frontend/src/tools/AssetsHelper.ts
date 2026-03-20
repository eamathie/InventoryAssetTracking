export const assetsAllRequest = async () => {
    const url = new URL("../Asset", import.meta.env.VITE_BACKEND_BASE_URL_HTTPS).href
    const response = await fetch(url, {
        method: 'GET',
        credentials: 'include',
    })

    if (!response.ok) {
        throw new Error(`Response status: ${response.status}`)
    }

    return response.json()
}

export const assetsByUser = async (userId: string) => {
    const url = new URL(`../Assets/${userId}`, import.meta.env.VITE_BACKEND_BASE_URL_HTTPS).href
    const response = await fetch(url, {
        method: 'GET',
        credentials: 'include'
    })

    if (!response.ok) {
        throw new Error(`Response status: ${response.status}`)
    }

    return response.json() 
}