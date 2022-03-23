import {useEffect, useState} from "react";
import Spinner from "../components/Spinner";

const Potions = () => {
    const [potions, setPotions] = useState();
    const [potionName, setPotionName] = useState("");
    const [studentName, setStudentName] = useState("");
    const [ingredients, setIngredients] = useState();
    const [chosenIngredient, setChosenIngredient] = useState("Chose ingredient");
    const [ingredientsId, setIngredientsId] = useState([]);
    const [isFormHidden, setIsFormHidden] = useState(true);

    useEffect(()=>{
        if (!ingredients)
        {
            fetch("ingredients")
                .then(respone=>respone.json())
                .then(data=>
                {
                    setIngredients(data);
                })
        }
    },[ingredients])

    useEffect(async () => {
        if (!potions) {
            const response = await fetch("potions");
            if (response.ok)
            {
                await response.json()
                    .then(data=>{
                        setPotions(data);
                    })
            }
            else {
                await response.text()
                    .then(data=>{
                        setPotions(data);
                    })
            }
        }
    }, [potions])

    const addPotion = async (e) => {
        e.preventDefault()
        const data = {
            makerName: studentName,
            potionName: potionName,
            ingredientsId: ingredientsId
        }
        const response = await fetch('potions', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
        setPotions();
        setIngredientsId([]);
        setPotionName("");
        setStudentName("");
        setIsFormHidden(true);
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
    const handleSelectChange = (e) => {
        setIngredientsId(prev=>[...prev, e.target.value.toString()]);
        setChosenIngredient(e.target.value)
    }
    const removeIngredient = (id) => {
        setIngredientsId(ingredientsId.filter(ingredientId => ingredientId !== id))
    }

    return (
        <div className="d-flex flex-column align-items-center mt-4">
            <h1>All potions</h1>
            <button className="btn btn-light my-4" onClick={() => setIsFormHidden(prev => !prev)}>Add New Potion
            </button>
            {!isFormHidden &&
                <form className="d-flex flex-column align-items-center p-5 bg-light rounded border border-dark w-40"
                      onSubmit={addPotion}>
                    <label className="mb-3">Maker name:
                        <input type="text" className="ms-3" value={studentName}
                               onChange={(e) => setStudentName(e.target.value)}/>
                    </label>
                    <label className="mb-3">Potion name:
                        <input type="text" className="ms-3" value={potionName}
                               onChange={(e) => setPotionName(e.target.value)}/>
                    </label>
                    <label className="mb-3">Ingredients:
                        <select className="ms-3" value={chosenIngredient}
                               onChange={(e)=>handleSelectChange(e)}>
                            {ingredients.map(ingredient=>
                                <option key={ingredient.id} value={ingredient.id}>{ingredient.name}</option>
                            )}
                        </select>

                    </label>
                    <div className="border p-3 mb-2 w-100 d-flex flex-column align-items-center flex-wrap">
                        <div className="mb-2 fw-bold">Selected ingredients:</div>
                        <div>
                            {ingredientsId.map(id=>
                                <button type="button" key={id} onClick={()=>removeIngredient(id)} className="btn btn-light">{ingredients.find(i=>i.id === id).name}</button>
                            )}
                        </div>
                    </div>
                    <button type="submit" className="btn btn-light w-20" disabled={ingredientsId.length < 5}>Add</button>
                </form>
            }
            <table className="table table-bordered table-hover w-70 mt-4 mx-auto mb-5">
                <thead>
                <tr>
                    <th className="w-5 text-center">#</th>
                    <th className="text-center">Name</th>
                    <th className="text-center">Maker</th>
                    <th className="w-35 text-center">Ingredients</th>
                    <th className="w-20 text-center">Brewing Status</th>
                    <th className="w-20 text-center">Recipe Name</th>
                </tr>
                </thead>
                <tbody>
                {potions && typeof potions !== "string" &&
                    potions.map((potion, index) =>
                        <tr key={potion.id}>
                            <td className="text-center align-middle">{index + 1}</td>
                            <td className="text-center align-middle">{potion.name}</td>
                            <td className="text-center align-middle">{potion.maker.name}</td>
                            <td className="text-start align-middle">{potion.ingredients.map((ingredient, index) => {
                                if (index === potion.ingredients.length - 1) {
                                    return <span key={ingredient.id} className="px-1">{ingredient.name}</span>
                                }
                                return <span key={ingredient.id} className="px-1">{ingredient.name},</span>
                            })}
                            </td>
                            <td className="text-center align-middle">{getCorrectBrewingStatus(potion.brewingStatus)}</td>
                            <td className="text-center align-middle">
                                {potion.brewingStatus !== 0 &&
                                    potion.recipe.name
                                }
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
            {potions && potions === "No potions" &&
                <h4>No potions yet</h4>
            }
            {!potions &&
                <Spinner/>
            }
        </div>
    );
};

export default Potions;
