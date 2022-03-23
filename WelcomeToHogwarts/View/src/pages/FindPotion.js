import {useState} from "react";

const FindPotion = () => {
    const [inputValue, setInputValue] = useState("");
    const [potions, setPotions] = useState();
    const [status, setStatus] = useState("");

    const searchForPotions = async (e) => {
        e.preventDefault();
        setStatus("")
        await fetch(`potions/search?search=${inputValue}`)
            .then(data=>{
                if (!data.ok)
                {
                    return setStatus("not found");
                }
                return data.json()})
            .then(potions=>{
                setPotions(potions);
                setInputValue("");
            })
    }

    const getCorrectBrewingStatus = (statusInt) => {
        switch (statusInt) {
            case 0:
                return "Brew"
            case 1:
                return "Replica"
            case 2:
                return "Discovery"
        }
    }

    return (
        <div className="d-flex flex-column align-items-center mt-5">
            <h1>Find a potion</h1>
            <form className="w-30 p-4 mt-3 d-flex flex-column align-items-center border rounded" onSubmit={searchForPotions}>
                <label className="w-100 d-flex flex-column align-items-center">
                    <div className="mb-2">Student / potion name</div>
                    <input type="text" value={inputValue} onChange={(e) => setInputValue(e.target.value)}/>
                </label>
                <button type="submit" className="btn btn-light w-15 mt-2">Search</button>
            </form>
            {status==="not found" &&
                <div className="mt-4 fw-bold">No potions matched the search results</div>
            }
            {potions &&
                <table className="table table-hover w-70 mt-4 mx-auto mb-5 table-bordered text-center ">
                    <thead>
                    <tr className="text-center">
                        <th>Potion name</th>
                        <th>Maker</th>
                        <th className="text-center w-30">Ingredients</th>
                        <th className="w-10">Brew status</th>
                        <th>Recipe name</th>
                    </tr>
                    </thead>
                    <tbody>
                    {potions.map(potion =>
                        <tr key={potion.id}>
                            <td>{potion.name}</td>
                            <td>{potion.maker.name}</td>
                            <td>{potion.ingredients.map(ingredient=>
                                <span key={ingredient.id} className="px-2">{ingredient.name}</span>
                            )}</td>
                            <td>{getCorrectBrewingStatus(potion.brewingStatus)}</td>
                            <td>{ potion.recipe &&
                                potion.recipe.name
                            }</td>
                        </tr>
                    )}
                    </tbody>
                </table>

            }
        </div>
    );
};

export default FindPotion;
