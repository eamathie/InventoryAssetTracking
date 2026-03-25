export const categoriesAllRequest = async () => {
    const url = new URL("../Category", import.meta.env.VITE_BACKEND_BASE_URL_HTTPS).href
    const response = await fetch(url, {
        method: 'GET',
        credentials: 'include'
    })

    if (!response.ok) {
        throw new Error(`Response status: ${response.status}`)
    }

    return response.json()
} 

export const categoryPatch = async (id: number, data: Record<string, string>) => {
    const url = new URL(`../Category/${id}`, import.meta.env.VITE_BACKEND_BASE_URL_HTTPS).href
    const response = await fetch(url, {
        method: 'PATCH',
        credentials: 'include',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })

    if (!response.ok) {
        throw new Error(`Response status: ${response.status}`)
    }

    return response.json()
} 