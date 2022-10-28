namespace AllSpiceSharp.Services;

public class RecipesService
{
  private readonly RecipesRepository _rr;

  public RecipesService(RecipesRepository rr)
  {
    _rr = rr;
  }

  internal List<Recipe> GetAllRecipes()
  {
    return _rr.GetAllRecipes();
  }

  internal Recipe CreateRecipe(Recipe newRecipe)
  {
    return _rr.CreateRecipe(newRecipe);
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _rr.GetRecipeById(recipeId);
    if (recipe == null)
    {
      throw new Exception("Recipe does not exist");
    }
    return recipe;
  }

  internal Recipe EditRecipe(Recipe recData)
  {
    Recipe current = GetRecipeById(recData.Id);
    current.Title = recData.Title ?? current.Title;
    current.Instructions = recData.Instructions ?? current.Instructions;
    current.Img = recData.Img ?? current.Img;
    current.Category = recData.Category ?? current.Category;
    return _rr.EditRecipe(current);
  }

  internal void RemoveRecipe(int recipeId, string accountId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != accountId)
    {
      throw new Exception("You can only delete your own recipes!");
    }
    _rr.RemoveRecipe(recipe);
  }
}