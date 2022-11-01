import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { Recipe } from "../models/Recipe.js"
import { api } from "./AxiosService.js"

class RecipesService {
  async getAllRecipes() {
    const res = await api.get('/api/recipes')
    AppState.recipes = res.data.map(r => new Recipe(r))
  }

  async setActiveRecipe(recipeId) {
    AppState.activeRecipe = null
    AppState.activeRecipe = AppState.recipes.find(r => r.id == recipeId)
    if (AppState.activeRecipe) {
      const res = await api.get(`/api/recipes/${AppState.activeRecipe.id}/ingredients`)
      AppState.ingredients = res.data.map(n => new Ingredient(n))
    }
  }

}

export const recipesService = new RecipesService()