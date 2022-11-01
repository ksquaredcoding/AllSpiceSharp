import { AppState } from "../AppState.js"
import { FavRecipe } from "../models/FavRecipe.js"
import Pop from "../utils/Pop.js";
import { api } from "./AxiosService.js"

class FavoritesService {
  async getFavorites() {
    const res = await api.get("/account/favorites")
    AppState.favRecipes = res.data.map(f => new FavRecipe(f));
  }

  async changeFav(recipeId) {
    const recipe = AppState.recipes.find(r => r.id == recipeId)
    if (!recipe) {
      Pop.error("Could not find recipe")
      return
    }
    const fav = AppState.favRecipes.find(f => f.recipeId == recipe.id)
    if (!fav) {
      let newFav = {}
      newFav.recipeId = recipe.id
      await api.post("/api/favorites", newFav)
      await this.getFavorites()
      return
    } else {
      await api.delete(`/api/favorites/${fav.favoriteId}`)
      await this.getFavorites()
      return
    }
  }
}

export const favoritesService = new FavoritesService()