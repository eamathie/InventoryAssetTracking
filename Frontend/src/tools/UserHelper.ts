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

export const usersAll = async () => {
    const url = new URL("../Auth", import.meta.env.VITE_BACKEND_BASE_URL_HTTPS).href
    const response = await fetch(url, {
        method: 'GET',
        credentials: 'include'
    })

    if (!response.ok) {
        throw new Error(`Response status: ${response.status}`)
    }

    return response.json()
}

export const userPatch = async (userId: string, data: Record<string, string>) => {
    const url = new URL(`../Auth/update?id=${userId}`, import.meta.env.VITE_BACKEND_BASE_URL_HTTPS).href
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