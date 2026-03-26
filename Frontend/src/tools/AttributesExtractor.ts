import type { Asset, User, Category, Checkout } from "../components/admin/AdminPanels"

export const filterItemAttributes = (item: Asset | User | Category | Checkout, excludedKeys: string[]) => {
    const attributes = Object.fromEntries(Object.entries(item).filter(([key]) => !excludedKeys.includes(key)))
    return attributes
}

export const includeItemAttributes = (item: Asset | User | Category | Checkout, includedKeys: string[]) => {
    const attributes = Object.fromEntries(Object.entries(item).filter(([key]) => includedKeys.includes(key)))
    return attributes
}