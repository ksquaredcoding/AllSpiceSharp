import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { api } from "./AxiosService.js"

class IngredientsService {
  async addIngredient(ingredientData) {
    ingredientData.recipeId = AppState.activeRecipe.id
    const res = await api.post("api/ingredients", ingredientData)
    const ingred = new Ingredient(res.data)
    AppState.ingredients.push(ingred)
  }
}

export const ingredientsService = new IngredientsService()