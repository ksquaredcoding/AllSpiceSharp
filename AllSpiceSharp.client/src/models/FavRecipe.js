export class FavRecipe {
  constructor(data) {
    this.favoriteId = data.favoriteId
    this.recipeId = data.recipeId
    this.accountId = data.accountId
  }
}