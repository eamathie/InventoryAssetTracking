interface AssetProps {
    name: string
    categoryId: number
    status: string
    purchaseDate: Date
    userId: string
    notes: string
}

const Asset = ({ name, categoryId, status, purchaseDate, userId, notes }: AssetProps) => {
    return(
        <div>
            <div>
                {name}
            </div>

            <div>
                {status}
            </div>

            <div>
                {purchaseDate.toString()}
            </div>

            <div>
                {notes}
            </div>
        </div>
    )
}

export default Asset