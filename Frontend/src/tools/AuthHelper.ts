export const authRequest = async (url: string, data: Record<string, string>) => {
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        })
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`)
        }
        return response
        
    } catch (error) {
        console.error(error)
    }
}