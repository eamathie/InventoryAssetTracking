import { useEffect } from "react"
import type { CategoryResponse } from "./Categories"

type CategoryProps = CategoryResponse & {
  onClick: () => void
}

const Category = ( {id, name, assets, onClick}: CategoryProps ) => {

    useEffect(() => {
        console.log("was called")
    }, [])

    useEffect(() => {
        console.log(name)
    }, [name])

    return(
        <div onClick={onClick} className="text-sm rounded-lg shadow-xl px-6 py-6 max-w-3xs flex-col cursor-pointer w-full bg-white hover:bg-gray-200">
            <div className="font-bold">
                <h2>{name}</h2>
            </div>

            <div>
            </div>
        </div>
    )
}

export default Category