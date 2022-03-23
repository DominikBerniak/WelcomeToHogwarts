import {useEffect, useState} from "react";
import Spinner from "../components/Spinner";

const Ingredients = () => {
    const [ingredients, setIngredients] = useState();
    const [newIngredient, setNewIngredient] = useState("");
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

    const addIngredient = async(e) => {
        e.preventDefault()
        const data = {
            name: newIngredient
        }
        const response = await fetch('ingredients', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
        setIngredients();
        setNewIngredient("")
        setIsFormHidden(true)
    }

    return (
        <div className="d-flex flex-column align-items-center mt-4">
            <h1>All ingredients</h1>
            <button className="btn btn-light my-4" onClick={()=>setIsFormHidden(prev=>!prev)}>Add Ingredient</button>
            {!isFormHidden &&
                <form className="d-flex flex-column align-items-center p-5 bg-light rounded border border-dark" onSubmit={addIngredient}>
                    <label className="mb-3">Ingredient name:
                        <input type="text" className="ms-3" value={newIngredient} onChange={(e)=>setNewIngredient(e.target.value)}/>
                    </label>
                    <button type="submit" className="btn btn-light w-20">Add</button>
                </form>
            }
            <table className="table table-hover w-30 mt-4 mx-auto mb-5">
                <thead>
                <tr>
                    <th className="w-20 text-center">#</th>
                    <th className="text-center">Ingredient</th>
                </tr>
                </thead>
                <tbody>
                {ingredients &&
                    ingredients.map((ingredient, index) =>
                        <tr key={ingredient.id}>
                            <td className="text-center align-middle">{index+1}</td>
                            <td className="text-center align-middle">{ingredient.name}</td>
                        </tr>
                    )}
                </tbody>
            </table>
            {!ingredients &&
                <Spinner />
            }
        </div>
    );
};

export default Ingredients;
