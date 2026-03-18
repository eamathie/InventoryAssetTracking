export const authRequest = async (url: string, data: Record<string, string>) => {
    const response = await fetch(url, {
        method: 'POST',
        credentials: 'include',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
    if (!response.ok) {
        throw new Error(`Response status: ${response.status}`)
    }

    return response
}