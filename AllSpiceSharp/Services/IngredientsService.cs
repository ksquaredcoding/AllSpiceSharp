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
}