import { Recipe } from "./Recipe.js"

export class FavRecipe extends Recipe {
  constructor(data) {
    super(data)
    this.favoriteId = data.favoriteId
    this.recipeId = data.recipeId
    this.accountId = data.accountId
  }
}