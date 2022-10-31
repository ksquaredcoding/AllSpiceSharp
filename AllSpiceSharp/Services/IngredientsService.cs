namespace AllSpiceSharp.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _ir;

  public IngredientsService(IngredientsRepository ir)
  {
    _ir = ir;
  }

  internal Ingredient CreateIngredient(Ingredient newIngredient)
  {
    return _ir.CreateIngredient(newIngredient);
  }

  internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
  {
    return _ir.GetIngredientsByRecipe(recipeId);
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _ir.GetIngredientById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception("Ingredient does not exist");
    }
    return ingredient;
  }

  internal void RemoveIngredient(int ingredientId, string accountId)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    if (ingredient.Recipe.CreatorId != accountId)
    {
      throw new Exception("You can only delete ingredients from your own recipes!");
    }
    _ir.RemoveIngredient(ingredient);
  }
}