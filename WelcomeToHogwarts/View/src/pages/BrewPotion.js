import {useEffect, useState} from "react";

const BrewPotion = () => {
    const [studentName, setStudentName] = useState("");
    const [potionName, setPotionName] = useState("");
    const [brewType, setBrewType] = useState("Add New Potion")
    const [ingredients, setIngredients] = useState();
    const [ingredient, setIngredient] = useState("");
    const [status, setStatus] = useState("");
    const [potionInfo, setPotionInfo] = useState();
    const [recipes, setRecipes] = useState();
    const [numberOfSimilarPotions, setNumberOfSimilarPotions] = useState(0);
    useEffect(() => {
        if (!ingredients) {
            fetch("ingredients")
                .then(respone => respone.json())
                .then(data => {
                    setIngredients(data);
                })
        }
    }, [ingredients])

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (brewType === "Add New Potion") {
            await brewNewPotion();
        } else {
            await addIngredient();
        }
    }

    const brewNewPotion = async () => {
        const data = {
            makerName: studentName,
            potionName: potionName
        }
        await fetch(`potions/brew`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
            .then(data => data.json())
            .then(potion => {
                setStatus("potion added");
                setBrewType("Add Ingredients");
                setPotionInfo(potion)
            })
    }

    const addIngredient = async () => {
        const res = await fetch(`potions/search/${potionName}`);
        const potion = await res.json();
        const data = {
            name: ingredient
        };
        await fetch(`potions/${potion.id}/add`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        })
            .then(data => data.json())
            .then(potion => {
                let brewStatus = getCorrectBrewingStatus(potion.brewingStatus);
                if (brewStatus === "Discovery") {
                    setStatus(brewStatus);
                }
                else if(brewStatus === "Replica")
                {
                    setStatus(brewStatus);
                    setSimilarPotions(potion.recipe.id);
                }
                else {
                    setStatus("ingredient added");
                }
                setPotionInfo(potion);
            })
    }

    const setSimilarPotions = async (recipeId) => {
        await fetch(`potions/recipes/${recipeId}`)
            .then(data=>data.json())
            .then(recipes=>{
                setNumberOfSimilarPotions(recipes.length);
            })
    }

    const discoveryMessages = ["Congratulations! You've discovered a new recipe", "Amazing! You've found a new recipe", "Superb! You've discovered a new potion recipe"];
    const getCorrectMessage = () => {
        switch (status) {
            case "potion added":
                return <>
                    <p>New potion created!</p>
                    <p>Time to add some ingredients</p>
                </>
            case "ingredient added":
                return <>
                    <p>New ingredient added!</p>
                </>
            case "Discovery":
                return <>
                    <p className="bg-danger text-white px-3">{discoveryMessages[Math.floor(Math.random() * discoveryMessages.length)]}</p>
                </>
            case "Replica":
                return <>
                    <p>Your new potion is a replica.</p>
                    <p>Number of potions of the same recipe: {numberOfSimilarPotions}</p>
                </>
        }
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
    const loadPotionData = async () => {
        await fetch(`potions/search/${potionName}`)
            .then(data => data.json())
            .then(potion => {
                setPotionInfo(potion);
                setStudentName(potion.maker.name)
            })
    }
    const getSimilarRecipes = async () => {
        const res = await fetch(`potions/search/${potionName}`);
        const potion = await res.json();
        const recipesRes = await fetch(`recipes/${potion.id}/help`)
        return await recipesRes.json();
    }
    const setSimilarRecipes = async () => {
        await getSimilarRecipes()
            .then(recipes=>{
                setRecipes(recipes);
            })
    }

    const changeFormType = (type) => {
        if (type === "add potion" && brewType !== "Add New Potion")
        {
            setBrewType("Add New Potion");
            setStatus("");
            setPotionInfo();
            setRecipes();
        }
        else if (type === "add ingredients" && brewType !== "Add Ingredients")
        {
            setBrewType("Add Ingredients");
            setStatus("");
            setPotionInfo();
            setRecipes();
        }
    }

    return (
        <div className="d-flex flex-column align-items-center mt-4">
            <h1 className="mb-4">Brew a potion</h1>
            <div className="d-flex w-30 justify-content-center border rounded py-3">
                <button className="btn btn-light me-5" onClick={() => changeFormType("add potion")}>Brew a new potion
                </button>
                <button className="btn btn-light" onClick={() => changeFormType("add ingredients")}>Add ingredients to an
                    existing potion
                </button>
            </div>
            <div className="d-flex flex-column align-items-center mt-3 fw-bold">
                {getCorrectMessage()}
            </div>
            {potionInfo &&
                <div className="d-flex flex-column align-items-center border w-30 p-3 mb-3">
                    <div>Potion name: {potionInfo.name}</div>
                    <div>Brew type: {getCorrectBrewingStatus(potionInfo.brewingStatus)}</div>
                    {potionInfo.ingredients &&
                        <div className="text-center mt-2">Ingredients added ({potionInfo.ingredients.length}/5):
                            <div>
                                {potionInfo.ingredients.map(ingredient =>
                                    <span key={ingredient.id} className="px-2">{ingredient.name}</span>
                                )}
                            </div>
                        </div>
                    }
                </div>
            }
            <form onSubmit={handleSubmit} className="d-flex flex-column align-items-center border rounded w-20 py-5">
                <label className="text-center mb-3 d-flex flex-column align-items-center w-100">Student name:
                    <input className="w-70 p-1" type="text" value={studentName}
                           onChange={(e) => setStudentName(e.target.value)}/>
                </label>
                <label className="text-center mb-3 d-flex flex-column align-items-center w-100">Potion name:
                    <input className="w-70 p-1" type="text" value={potionName}
                           onChange={(e) => setPotionName(e.target.value)}/>
                </label>
                {brewType === "Add Ingredients" &&
                    <>
                        <label className="d-flex flex-column align-items-center w-100 mb-3">Ingredients:
                            <select className="w-70 p-1" value={ingredient}
                                    onChange={(e) => setIngredient(e.target.value)}>
                                {ingredients.map(ingredient =>
                                    <option key={ingredient.id} value={ingredient.name}>{ingredient.name}</option>
                                )}
                            </select>
                        </label>
                        <label className="text-center mb-3 d-flex flex-column align-items-center w-100">Add New
                            Ingredient:
                            <input className="w-70 p-1" type="text" value={ingredient}
                                   onChange={(e) => setIngredient(e.target.value)}/>
                        </label>
                    </>
                }
                {brewType === "Add Ingredients" && potionName !== "" &&
                    <button className="btn btn-light mt-2 w-50" type="button" onClick={loadPotionData}>Load
                        potion</button>
                }
                {brewType === "Add Ingredients" && studentName !== "" && potionName !== "" &&
                    <button className="btn btn-light mt-3 w-50" type="button" onClick={setSimilarRecipes}>Show similar
                        recipes</button>
                }
                <button className="btn btn-light mt-4 w-50" type="submit">{brewType}</button>

            </form>
            {recipes &&
                <div id="similar-recipes" className="w-35">
                    <h3 className="text-center mb-3">Similar recipes</h3>
                    <table className="border ">
                        <thead>
                        <tr className="text-center">
                            <th className="w-40 p-3">Recipe Name</th>
                            <th>Ingredients</th>
                        </tr>
                        </thead>
                        <tbody>
                        {recipes.map(recipe =>
                            <tr key={recipe.id} className="border">
                                <td className="p-3">{recipe.name}</td>
                                <td className="text-center py-2">
                                    {recipe.ingredients.map(ingredient =>
                                        <div key={ingredient.id}>{ingredient.name}</div>
                                    )}
                                </td>
                            </tr>
                        )}
                        </tbody>
                    </table>
                </div>
            }
        </div>
    );
};

export default BrewPotion;
