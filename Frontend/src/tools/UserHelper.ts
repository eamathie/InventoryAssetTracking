export const userById = async (userId: string) => {
    const url = new URL(`../Auth/${userId}`, import.meta.env.VITE_BACKEND_BASE_URL_HTTPS).href
    const response = await fetch(url, {
        method: 'GET',
        credentials: 'include'
    })

    if (!response.ok) {
        throw new Error(`Response status: ${response.status}`)
    }

    return response.json()
}

export const userRoles = async () => {
    const url = new URL(`../Auth/roles`, import.meta.env.VITE_BACKEND_BASE_URL_HTTPS).href
    const response = await fetch(url, {
        method: 'GET',
        credentials: 'include'
    })

    if (!response.ok) {
        throw new Error(`Response status: ${response.status}`)
    }

    return response.json()
}