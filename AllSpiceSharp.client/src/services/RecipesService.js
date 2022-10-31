import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"
import { api } from "./AxiosService.js"

class RecipesService {
  async getAllRecipes() {
    const res = await api.get('/api/recipes')
    AppState.recipes = res.data.map(r => new Recipe(r))
    console.log(AppState.recipes);
  }
}

export const recipesService = new RecipesService()